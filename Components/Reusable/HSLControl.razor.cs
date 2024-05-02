using CustomPomodoro.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Reusable
{
    public partial class HSLControl
    {
        [Parameter]
        public HSLColor Color { get; set; }

        [Parameter]
        public string Header { get; set; } = string.Empty;

        [Parameter]
        public bool IsPrimaryColorSelector { get; set; }

        private string HueVisibilityString { get; set; } = string.Empty;

        private string SatAndLightVisibilityString { get; set; } = string.Empty;

        private const string HTMLShowKeyword = "visible";
        private const string HTMLHideKeyword = "invisible";

        //NOT working. Will probably need some type of event handler
        //tied between child & parent components.
        protected override async Task OnInitializedAsync()
        {
            if (IsPrimaryColorSelector)
                HueVisibilityString = HTMLShowKeyword;
            else
                HueVisibilityString = HTMLHideKeyword;

            SatAndLightVisibilityString = HTMLHideKeyword;
        }
        public void DisplayHueControlsOnly()
        {
            SatAndLightVisibilityString = HTMLHideKeyword;
        }
        public void DisplayAllHslColorControls()
        {
            HueVisibilityString = HTMLShowKeyword;
            SatAndLightVisibilityString = HTMLShowKeyword;
        }

        //NOT fully implemented.
        public void HideAllHslControls()
        {
            HueVisibilityString = HTMLHideKeyword;
            SatAndLightVisibilityString = HTMLHideKeyword;
        }
    }
}
