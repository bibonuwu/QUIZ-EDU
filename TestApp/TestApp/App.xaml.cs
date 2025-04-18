using System.Collections.Generic;
using System;
using System.Windows;

namespace TestApp
{
    public partial class App : Application
    {
    }

    public static class WindowManager
    {
        private static Dictionary<Type, Window> openWindows = new Dictionary<Type, Window>();

        public static void ShowWindow<T>(Func<T> createWindow) where T : Window
        {
            if (openWindows.TryGetValue(typeof(T), out Window existingWindow))
            {
                if (existingWindow != null && existingWindow.IsVisible)
                {
                    existingWindow.Activate(); // Переносит окно на передний план
                    return;
                }
            }

            var newWindow = createWindow();
            openWindows[typeof(T)] = newWindow;

            newWindow.Closed += (s, e) => openWindows.Remove(typeof(T));
            newWindow.Show();
        }
    }
}
