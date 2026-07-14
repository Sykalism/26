using System.Collections.Generic;
public class PanelStack
{
    public Stack<UIPanel> panels;

    public PanelStack()
    {
        panels = new Stack<UIPanel>();
    }

    public void Open(UIPanel panel)
    {
        if (panels.Count > 0) panels.Peek().Hide();

        panels.Push(panel);
        panel.Show();
    }
    public void Back()
    {
        if (panels.Count == 0) return;

        panels.Pop().Hide();

        if (panels.Count > 0) panels.Peek().Show();
    }
}
