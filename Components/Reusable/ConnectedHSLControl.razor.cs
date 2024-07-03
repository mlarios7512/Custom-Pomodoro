using CustomPomodoro.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Reusable
{
    public partial class ConnectedHSLControl
    {
        [Parameter]
        public HSLColor FirstColor { get; set; }

        [Parameter]
        public HSLColor SecondColor { get; set; }

        [Parameter]
        public EventCallback OnInteract { get; set; }

        public bool IsPrimaryColorSelector { get; set; } = true;

        private string HueVisibilityString { get; set; } = string.Empty;

        private string SatAndLightVisibilityString { get; set; } = string.Empty;

        private const string HTMLShowKeyword = "visible";
        private const string HTMLHideKeyword = "invisible";

        protected override async Task OnInitializedAsync()
        {
            if (IsPrimaryColorSelector)
                HueVisibilityString = HTMLShowKeyword;
            else
                HueVisibilityString = HTMLHideKeyword;

            SatAndLightVisibilityString = HTMLHideKeyword;
        }

        private async Task SyncHuesIfNeeded()
        {
            Debug.WriteLine("Interaction with 1st hue occuring...");
            if (OnInteract.HasDelegate)
            {
                Debug.WriteLine("Syncing 1st & 2nd hues...");
                await OnInteract.InvokeAsync();
            }
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
