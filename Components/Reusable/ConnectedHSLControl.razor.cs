using CustomPomodoro.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
        public string FirstColorTitle { get; set; }
        [Parameter]
        public string SecondColorTitle { get; set;}

        [Parameter]
        public EventCallback OnInteract { get; set; }
        private string FirstHueVisibilityString { get; set; } = string.Empty;
        private string SecondHueVisibilityString { get; set; } = string.Empty ;

        private string SatAndLightVisibilityString { get; set; } = string.Empty;

        private const string HTMLShowKeyword = "visible";
        private const string HTMLHideOnlyKeyword = "invisible";
        private const string HTMLHideAndMinimizedKeyword = "hidden-and-minimized";

        protected override async Task OnInitializedAsync()
        {
            FirstHueVisibilityString = HTMLShowKeyword;
            SecondHueVisibilityString = HTMLHideOnlyKeyword;
            SatAndLightVisibilityString = HTMLHideAndMinimizedKeyword;
        }

        private async Task SyncHuesIfNeededMouse(MouseEventArgs e)
        {
            //Checking if left button was held down when this was occuring.
            if (e.Buttons == 1)
            {
                if (OnInteract.HasDelegate)
                    await OnInteract.InvokeAsync();
            }
        }

        //Note: There is a glitch with this.
        //If you touch the scroll bar (but do NOT drag it), then the 1st hue will change color, but the 2nd one will not. (at least within android emulator) 
        //Maybe this could help: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.web.toucheventargs?view=aspnetcore-8.0
        private async Task SyncHueIfNeededMobile() 
        {
            if (OnInteract.HasDelegate)
                await OnInteract.InvokeAsync();
        }

        public void DisplayHueControlsOnly()
        {
            SecondHueVisibilityString = HTMLHideOnlyKeyword;
            SatAndLightVisibilityString = HTMLHideAndMinimizedKeyword;
        }
        public void DisplayAllHslColorControls()
        {
            FirstHueVisibilityString = HTMLShowKeyword;
            SecondHueVisibilityString = HTMLShowKeyword;
            SatAndLightVisibilityString = HTMLShowKeyword;
        }
    }
}
