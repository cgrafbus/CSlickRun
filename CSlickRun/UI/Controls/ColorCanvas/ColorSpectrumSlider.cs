/*************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2024 Xceed Software Inc.

   This program is provided to you under the terms of the XCEED SOFTWARE, INC.
   COMMUNITY LICENSE AGREEMENT (for non-commercial use) as published at
   https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CSlickRun.UI.Controls.ColorCanvas;

[TemplatePart(Name = PART_SpectrumDisplay, Type = typeof(Rectangle))]
public class ColorSpectrumSlider : Slider
{
    private const string PART_SpectrumDisplay = "PART_SpectrumDisplay";

    public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(nameof(SelectedColor),
        typeof(Color), typeof(ColorSpectrumSlider), new PropertyMetadata(Colors.Transparent));

    private LinearGradientBrush _pickerBrush;

    private Rectangle _spectrumDisplay;

    static ColorSpectrumSlider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSpectrumSlider),
            new FrameworkPropertyMetadata(typeof(ColorSpectrumSlider)));
    }

    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _spectrumDisplay = (Rectangle)GetTemplateChild(PART_SpectrumDisplay);
        CreateSpectrum();
        OnValueChanged(double.NaN, Value);
    }

    protected override void OnValueChanged(double oldValue, double newValue)
    {
        base.OnValueChanged(oldValue, newValue);

        var color = ColorUtilities.ConvertHsvToRgb(360 - newValue, 1, 1);
        SelectedColor = color;
    }

    private void CreateSpectrum()
    {
        _pickerBrush = new LinearGradientBrush();
        _pickerBrush.StartPoint = new Point(0.5, 0);
        _pickerBrush.EndPoint = new Point(0.5, 1);
        _pickerBrush.ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation;

        var colorsList = ColorUtilities.GenerateHsvSpectrum();

        var stopIncrement = (double)1 / (colorsList.Count - 1);

        int i;
        for (i = 0; i < colorsList.Count; i++)
            _pickerBrush.GradientStops.Add(new GradientStop(colorsList[i], i * stopIncrement));

        _pickerBrush.GradientStops[i - 1].Offset = 1.0;
        _spectrumDisplay.Fill = _pickerBrush;
    }
}