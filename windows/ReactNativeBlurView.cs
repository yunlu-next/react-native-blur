using System;
using Microsoft.ReactNative;
using Microsoft.ReactNative.Managed;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ReactNativeBlur
{
    public sealed class ReactNativeBlurView : Grid
    {
        private readonly Grid backdropLayer;
        private readonly Grid overlayLayer;
        private readonly Grid contentLayer;
        private readonly AcrylicBrush acrylicBrush;

        private string blurType = "xlight";
        private double blurAmount = 10.0;
        private Color? customTintColor;
        private Color? customFallbackColor;

        public ReactNativeBlurView()
        {
            acrylicBrush = new AcrylicBrush
            {
                BackgroundSource = AcrylicBackgroundSource.Backdrop,
                AlwaysUseFallback = false,
            };

            backdropLayer = new Grid
            {
                Background = acrylicBrush,
                IsHitTestVisible = false,
            };
            overlayLayer = new Grid
            {
                IsHitTestVisible = false,
            };
            contentLayer = new Grid();

            Children.Add(backdropLayer);
            Children.Add(overlayLayer);
            Children.Add(contentLayer);

            UpdateBrush();
        }

        public void SetBlurType(string value)
        {
            blurType = string.IsNullOrWhiteSpace(value) ? "xlight" : value;
            UpdateBrush();
        }

        public void SetBlurAmount(double value)
        {
            blurAmount = Math.Min(Math.Max(value, 0.0), 100.0);
            UpdateBrush();
        }

        public void SetTintColor(Brush brush)
        {
            if (brush is SolidColorBrush solidColorBrush)
            {
                customTintColor = solidColorBrush.Color;
                UpdateBrush();
            }
        }

        public void SetOverlayColor(Brush brush)
        {
            overlayLayer.Background = brush;
        }

        public void SetReducedTransparencyFallbackColor(Brush brush)
        {
            if (brush is SolidColorBrush solidColorBrush)
            {
                customFallbackColor = solidColorBrush.Color;
                UpdateBrush();
            }
        }

        public void InsertReactChild(UIElement child, int index)
        {
            contentLayer.Children.Insert(index, child);
        }

        public void ClearReactChildren()
        {
            contentLayer.Children.Clear();
        }

        public void RemoveReactChildAt(int index)
        {
            contentLayer.Children.RemoveAt(index);
        }

        public void ReplaceReactChild(UIElement oldChild, UIElement newChild)
        {
            var index = contentLayer.Children.IndexOf(oldChild);

            if (index >= 0)
            {
                contentLayer.Children.RemoveAt(index);
                contentLayer.Children.Insert(index, newChild);
            }
        }

        private void UpdateBrush()
        {
            var palette = GetPalette(blurType);
            var tint = customTintColor ?? palette.TintColor;

            acrylicBrush.TintColor = tint;
            acrylicBrush.FallbackColor = customFallbackColor ?? tint;
            acrylicBrush.TintOpacity = Math.Min(
                Math.Max(palette.BaseTintOpacity + blurAmount / 220.0, 0.0),
                0.85);
            acrylicBrush.TintLuminosityOpacity = palette.LuminosityOpacity;
        }

        private static BlurPalette GetPalette(string type)
        {
            switch (type)
            {
                case "dark":
                case "extraDark":
                case "systemUltraThinMaterialDark":
                case "systemThinMaterialDark":
                case "systemMaterialDark":
                case "systemThickMaterialDark":
                case "systemChromeMaterialDark":
                    return new BlurPalette(Color.FromArgb(0xB8, 0x0F, 0x17, 0x2A), 0.30, 0.35);
                case "light":
                case "systemUltraThinMaterialLight":
                case "systemThinMaterialLight":
                case "systemMaterialLight":
                case "systemThickMaterialLight":
                case "systemChromeMaterialLight":
                    return new BlurPalette(Color.FromArgb(0xB8, 0xFF, 0xFF, 0xFF), 0.18, 0.78);
                case "prominent":
                case "systemThickMaterial":
                case "systemChromeMaterial":
                    return new BlurPalette(Color.FromArgb(0xCC, 0xFF, 0xFF, 0xFF), 0.28, 0.95);
                case "regular":
                case "systemMaterial":
                    return new BlurPalette(Color.FromArgb(0xB8, 0xF8, 0xFA, 0xFC), 0.20, 0.72);
                case "xlight":
                case "systemUltraThinMaterial":
                case "systemThinMaterial":
                default:
                    return new BlurPalette(Color.FromArgb(0xCC, 0xFF, 0xFF, 0xFF), 0.22, 0.85);
            }
        }

        private readonly struct BlurPalette
        {
            public BlurPalette(Color tintColor, double baseTintOpacity, double luminosityOpacity)
            {
                TintColor = tintColor;
                BaseTintOpacity = baseTintOpacity;
                LuminosityOpacity = luminosityOpacity;
            }

            public Color TintColor { get; }
            public double BaseTintOpacity { get; }
            public double LuminosityOpacity { get; }
        }
    }

    public sealed class ReactNativeBlurViewManager :
        AttributedViewManager<ReactNativeBlurView>,
        IViewManagerWithChildren
    {
        public override string Name => "ReactNativeBlurView";

        [ViewManagerProperty("blurType")]
        public void BlurType(ReactNativeBlurView view, string value)
        {
            view.SetBlurType(value);
        }

        [ViewManagerProperty("blurAmount")]
        public void BlurAmount(ReactNativeBlurView view, double value)
        {
            view.SetBlurAmount(value);
        }

        [ViewManagerProperty("blurRounds")]
        public void BlurRounds(ReactNativeBlurView view, int value)
        {
        }

        [ViewManagerProperty("ignoreSafeArea")]
        public void IgnoreSafeArea(ReactNativeBlurView view, bool value)
        {
        }

        [ViewManagerProperty("tintColor", ViewManagerPropertyType.Color)]
        public void TintColor(ReactNativeBlurView view, Brush value)
        {
            view.SetTintColor(value);
        }

        [ViewManagerProperty("overlayColor", ViewManagerPropertyType.Color)]
        public void OverlayColor(ReactNativeBlurView view, Brush value)
        {
            view.SetOverlayColor(value);
        }

        [ViewManagerProperty("reducedTransparencyFallbackColor", ViewManagerPropertyType.Color)]
        public void ReducedTransparencyFallbackColor(ReactNativeBlurView view, Brush value)
        {
            view.SetReducedTransparencyFallbackColor(value);
        }

        public void AddView(FrameworkElement parent, UIElement child, long index)
        {
            ((ReactNativeBlurView)parent).InsertReactChild(child, (int)index);
        }

        public void RemoveAllChildren(FrameworkElement parent)
        {
            ((ReactNativeBlurView)parent).ClearReactChildren();
        }

        public void RemoveChildAt(FrameworkElement parent, long index)
        {
            ((ReactNativeBlurView)parent).RemoveReactChildAt((int)index);
        }

        public void ReplaceChild(FrameworkElement parent, UIElement oldChild, UIElement newChild)
        {
            ((ReactNativeBlurView)parent).ReplaceReactChild(oldChild, newChild);
        }
    }

    public sealed class ReactPackageProvider : IReactPackageProvider
    {
        public void CreatePackage(IReactPackageBuilder packageBuilder)
        {
            packageBuilder.AddViewManager("ReactNativeBlurView", () => new ReactNativeBlurViewManager());
        }
    }
}
