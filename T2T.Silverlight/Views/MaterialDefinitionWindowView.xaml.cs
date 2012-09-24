using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using T2T.Model;


namespace T2T
{
    public partial class MaterialDefinitionWindowView : ChildWindow
    {
        ResourceDefinition _material;
        ResourceClass _class;

        public MaterialDefinitionWindowView(ResourceClass mclass)
        {
            InitializeComponent();

            _class = mclass;
            _material = CreateMaterialDefinition();

            dataForm.CurrentItem = _material;
        }

        private ResourceDefinition CreateMaterialDefinition()
        {
            if (_class != null)
            {
                switch (_class.Name)
                {
                    case "Lank":
                        return new Lank();
                    case "Raw Tyre":
                        return new ScooterRawTyre();
                    case "Ply":
                        return new Ply();
                    case "InnerLiner":
                        return new Inerliner();
                    case "Protektor":
                        return new Protector();
                    case "Carcass":
                        return new Carcass();
                    case "Tyre":
                        return new ScooterTyre();
                    default :
                        return null;
                       
                }
            }

            return null;
        }

        public ResourceDefinition Material { get { return _material; } }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

