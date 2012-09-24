using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace T2T.Controls
{
    public class SelectorDataForm : DataForm
    {
        public DataTemplateSelector EditItemTemplateSelector { get; set; }
        public DataTemplateSelector ReadOnlyItemTemplateSelector { get; set; }
        public DataTemplateSelector NewItemTemplateSelector { get; set; }

        public SelectorDataForm()
        {
            this.CurrentItemChanged += new EventHandler<EventArgs>(SelectorDataForm_CurrentItemChanged);
        }

        void SelectorDataForm_CurrentItemChanged(object sender, EventArgs e)
        {
            SelectTemplate();
        }

        private void SelectTemplate()
        {
            if (EditItemTemplateSelector != null)
                EditTemplate = EditItemTemplateSelector.SelectTemplate(this.CurrentItem);
            else
                EditTemplate = null;

            if (ReadOnlyItemTemplateSelector != null)
                ReadOnlyTemplate = ReadOnlyItemTemplateSelector.SelectTemplate(this.CurrentItem);
            else
                ReadOnlyTemplate = null;

            if (NewItemTemplateSelector != null)
                NewItemTemplate = NewItemTemplateSelector.SelectTemplate(this.CurrentItem);
            else
                NewItemTemplate = null;
        }
    }
}
