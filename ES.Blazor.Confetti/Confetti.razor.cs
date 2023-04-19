using Microsoft.AspNetCore.Components;

namespace ES.Blazor.Confetti;

public partial class Confetti
{
    [Parameter]
    public bool Visible { get; set; }

    public string ConfettiHideCss { get; set; } = "";
    public int Duration { get; set; }

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
        if (Visible)
        {
            return;
        }

        if (duration == 0)
        {
            Visible = true;
            await InvokeAsync(StateHasChanged);
        }
        else if (duration > 0)
        {
            Duration = (int)Math.Floor(duration);
            ConfettiHideCss = "confetti-hide";
            Visible = true;
            await InvokeAsync(StateHasChanged);
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(duration));

            while (await timer.WaitForNextTickAsync())
            {
                timer.Dispose();
            }

            //await InvokeAsync(StateHasChanged);
            timer = new PeriodicTimer(TimeSpan.FromSeconds(1.1));

            while (await timer.WaitForNextTickAsync())
            {
                Visible = false;
                ConfettiHideCss = "";
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
