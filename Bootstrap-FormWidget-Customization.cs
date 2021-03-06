using BootstrapLayoutTool;
using CMS;
using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;
using Kentico.Forms.Web.Mvc.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterModule(typeof(FormWidgetBootstrapLayout))]

namespace BootstrapLayoutTool
{
    public class FormWidgetBootstrapLayout : Module
    {
        // Module class constructor, the system registers the module under the name "CustomInit"
        public FormWidgetBootstrapLayout()
            : base("FormWidgetBootstrapLayout")
        {
        }

        // Contains initialization code that is executed when the application starts
        protected override void OnInit()
        {
            base.OnInit();
            // Modifies the default FormFieldRenderingConfiguration for the 'Form' widget
            // Specifying a new FormFieldRenderingConfiguration instance completely replaces the default Kentico configuration
            FormFieldRenderingConfiguration.Widget.RootConfiguration = null;
            FormFieldRenderingConfiguration.Widget.LabelWrapperConfiguration = null;
            FormFieldRenderingConfiguration.Widget.EditorWrapperConfiguration = null;
            FormFieldRenderingConfiguration.Widget.ComponentWrapperConfiguration = null;
            FormFieldRenderingConfiguration.Widget.ExplanationTextWrapperConfiguration = null;
            FormFieldRenderingConfiguration.Widget.ColonAfterLabel = false;
            FormFieldRenderingConfiguration.Widget.ExplanationTextWrapperConfiguration = new ElementRenderingConfiguration
            {
                ElementName = "small",
                HtmlAttributes = { { "class", "form-text text-muted" } } // can drop the "form-text" if doing inline
            };

            // Sets a new rendering configuration for the 'Form' widget, adding attributes
            // to the form element and the submit button and wrapping the form in two 'div' blocks
            FormWidgetRenderingConfiguration.Default = new FormWidgetRenderingConfiguration
            {
                // Submit button HTML attributes
                SubmitButtonHtmlAttributes = { { "class", "btn btn-primary" } },

            };

            // Customizations here
            FormFieldRenderingConfiguration.GetConfiguration.Execute += GetConfiguration_Execute1;
        }

        private void GetConfiguration_Execute1(object sender, GetFormFieldRenderingConfigurationEventArgs e)
        {
            string FormName = e.FormComponent.GetBizFormComponentContext().FormInfo.FormName;
            string FormFieldName = e.FormComponent.Name;

            switch (FormName)
            {
                // https://getbootstrap.com/docs/4.0/components/forms/#form-grid
                default:
                    // Set Bootstrap 4 Layout properties to:
                    // form-group Automation: On Column (or none if you do not want to use form groups)
                    // Columns: any #
                    // Set column sizes to whatever you wish
                    // Put various form elements within the columns
                    break;

                // https://getbootstrap.com/docs/4.0/components/forms/#horizontal-form
                // Set Bootstrap 4 Layout properties to:
                // form-group Automation: On Row
                // Columns: 1
                // 1st Column Size: Do not render column div
                // (put various form elements within the div)
                case "Example_LabelInputSideBySide":
                    switch (FormFieldName)
                    {
                        case "FirstName":
                        case "LastName":

                            e.Configuration.LabelHtmlAttributes["class"] = "col-sm-2";
                            e.Configuration.EditorWrapperConfiguration = new ElementRenderingConfiguration
                            {
                                ElementName = "div",
                                HtmlAttributes = { { "class", "col-sm-4" } }
                            };
                            break;
                        default:
                            e.Configuration.LabelHtmlAttributes["class"] = "col-sm-2";
                            e.Configuration.EditorWrapperConfiguration = new ElementRenderingConfiguration
                            {
                                ElementName = "div",
                                HtmlAttributes = { { "class", "col-sm-2" } }
                            };
                            break;
                    }
                    break;
                // https://getbootstrap.com/docs/4.0/components/forms/#inline-forms
                // Set Bootstrap 4 Layout properties to:
                // form-group Automation: none
                // Container CSS: form-inline
                // Columns: 1
                // 1st Column Size: Do not render column div
                // (put various form elements within the div)
                case "Example_FormInline":
                    e.Configuration.LabelHtmlAttributes["class"] = "sr-only";
                    e.Configuration.EditorHtmlAttributes["class"] = "form-control mb-2 mr-sm-2";
                    break;
            }

            // https://getbootstrap.com/docs/4.0/components/forms/#select-menu
            if (e.FormComponent is DropDownComponent)
            {
                if (e.Configuration.EditorHtmlAttributes.ContainsKey("class"))
                {
                    e.Configuration.EditorHtmlAttributes["class"] += " custom-select";
                }
                else
                {
                    e.Configuration.EditorHtmlAttributes["class"] = " custom-select";
                }
            }

        }
    }


}
