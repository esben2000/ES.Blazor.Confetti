using Microsoft.AspNetCore.Components;

namespace ES.Blazor.Confetti;

public partial class Confetti
{
    [Parameter]
    public bool Visible { get; set; }

    public string ConfettiHideCss { get; set; } = "";

    enum ConfettiColor
    {
        Red,
        Orange,
        LightGreen,
        Purple,
        Lightskyblue,
        Mediumvioletred
    }

    public async Task ShowAsync(double duration = 0)
    {
        Visible = true;
        
        await InvokeAsync(StateHasChanged);

        if (duration > 0)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(duration));

            while (await timer.WaitForNextTickAsync())
            {
                ConfettiHideCss = "confetti-hide";
                timer.Dispose();
            }

            await InvokeAsync(StateHasChanged);

            timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

            while (await timer.WaitForNextTickAsync())
            {
                ConfettiHideCss = "";
                Visible = false;
                timer.Dispose();
            }

            await InvokeAsync(StateHasChanged);
        }
    }

    public void Show()
    {
        Visible = true;
        StateHasChanged();
    }

    public async Task HideAsync()
    {
        Visible = false;
        await InvokeAsync(StateHasChanged);
    }

    public void Hide()
    {
        Visible = false;
        StateHasChanged();
    }
}