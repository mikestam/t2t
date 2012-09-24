using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.ServiceModel.DomainServices.Client;
using System.Linq;
using T2T.Web;

namespace T2T.Repositories
{
    /// <summary>
    /// Our base Repository aka DataService aka ServiceAgent 
    /// like ReceptureRepository aka ReceptureDataService aka ReceptureServiceAgent
    /// We can have one domainContext pre useCase or modul and one DataService or we can split it
    /// into few smaller ones.
    /// This is not real repository pattern in the sense of ModelDomain design, it's just wrapper over data context
    /// so we dont need to pass our domainContext deep into hierarchy and we can hide domainContext dependency and 
    /// persistance mechanism for our viewModel and View classes. We can acces entites in aggregates directly which should not be done 
    /// in modelDomain since they should not exist independantly from aggregate root. 
    /// This implementation is suitable when we have only one aggregate root editable at once, if we can edit
    /// multiple and diffrent aggregate roots better way is to go with instance repositories and DI container.
    /// OBSERVATION:
    /// If we use this instance repositories we need DI, otherwise we should use static facade!
    /// </summary>
    public abstract class RepositoryBase
    {
        private static ResourcesContext context = new ResourcesContext();

        protected IDictionary<object, bool> EntityLoadingOperations = new Dictionary<object, bool>();

        public ResourcesContext Context
        {
            get
            {
                return RepositoryBase.context;
            }
        }
        
        public void SaveOrUpdateEntities(Action callback = null)
        {
            System.Diagnostics.Debug.WriteLine("Request for SaveOrUpdates()");
            if (this.Context.IsSubmitting)
            {
                Debug.WriteLine("Already submitting. returning from SaveOrUpdateEntities.");
                return;
            }

            Debug.WriteLine("Executing Context.SubmitChanges()");
            var so = this.Context.SubmitChanges();
           
            EventHandler submitOperationCompleted = null;
            submitOperationCompleted = (s, e) =>
                {
                    // s(ender) is so
                    so.Completed -= submitOperationCompleted;
                    if (so.HasError)
                    {
                        this.LogErrorToDebug(so);
                        so.MarkErrorAsHandled();
                    }
                    if (callback != null)
                    {
                        callback();
                    }

                };
            so.Completed += submitOperationCompleted;

            // why not
            //Context.SubmitChanges( so1 =>
            //{
            //    if (so1.HasError)
            //    {
            //        this.LogErrorToDebug(so);
            //        so1.MarkErrorAsHandled();
            //    }
            //    if (callback != null)
            //    {
            //        callback();
            //    }

            //}, null);        
        }

        protected void LoadQuery<T>(EntityQuery<T> query, Action<IEnumerable<T>> callback)
           where T : Entity
        {
            var key = this.GenerateKeyForObject<T>(query, typeof(T));
            if (this.IsContextLoadingForObject(key))
            {
                return;
            }

            EventHandler onCompleted = null;
            onCompleted = (s, args) =>
            {
                var lo = s as LoadOperation<T>;
                callback(lo.Entities);
                lo.Completed -= onCompleted;
                this.EntityLoadingOperations[key] = false;
                System.Diagnostics.Debug.WriteLine("Loaded query for {0}", key);
            };

            this.EntityLoadingOperations[key] = true;
            var loadOperation = this.Context.Load<T>(query);
            loadOperation.Completed += onCompleted;
        }


        protected void LoadQuery<T>(EntityQuery<T> query, Action<T> callback)
        where T : Entity
        {
            Action<IEnumerable<T>> innerCallback = (o) =>
            {
                if (o == null || o.Count() == 0)
                {
                    callback(default(T));
                    return;
                }

                callback(o.First());
            };

            this.LoadQuery<T>(query, innerCallback);
        }

        protected bool IsContextLoadingForObject(object obj)
        {
            if (this.EntityLoadingOperations.Keys.Contains(obj))
            {
                System.Diagnostics.Debug.WriteLine("Testing {0} for loading - {1}", obj, this.EntityLoadingOperations[obj]);
                return this.EntityLoadingOperations[obj];
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Testing {0} for loading - {1}", obj, "ELSE - false");
                this.EntityLoadingOperations.Add(obj, false);
                return false;
            }
        }


        private object GenerateKeyForObject<T>(EntityQuery<T> query, Type t)
           where T : Entity
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append(query.QueryName);
            if (query.Parameters != null)
            {
                foreach (var param in query.Parameters)
                {
                    builder.Append(param);
                }
            }
            builder.Append(t.FullName);
            var result = builder.ToString();
            return result;
        }


        private void LogErrorToDebug(System.ServiceModel.DomainServices.Client.SubmitOperation so)
        {
            System.Diagnostics.Debug.WriteLine("ERROR: {0}", so.Error);

            foreach (var entity in so.EntitiesInError)
            {
                foreach (var error in entity.ValidationErrors)
                {
                    foreach (var memberNames in error.MemberNames)
                    {
                        System.Diagnostics.Debug.WriteLine("ERROR: {0} {1}...", error.ErrorMessage, memberNames);
                    }
                }
                if (entity.EntityConflict != null)
                {
                    foreach (var propertyName in entity.EntityConflict.PropertyNames)
                    {
                        System.Diagnostics.Debug.WriteLine("ERROR: {0} {1}...", entity, propertyName);
                    }
                }
            }
        }
    }
}
