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

using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CSlickRun.UI.Controls.ColorCanvas;

[TemplatePart(Name = PART_ColorShadingCanvas, Type = typeof(Canvas))]
[TemplatePart(Name = PART_ColorShadeSelector, Type = typeof(Canvas))]
[TemplatePart(Name = PART_SpectrumSlider, Type = typeof(ColorSpectrumSlider))]
[TemplatePart(Name = PART_HexadecimalTextBox, Type = typeof(TextBox))]
public class ColorCanvas : Control
{
    private const string PART_ColorShadingCanvas = "PART_ColorShadingCanvas";
    private const string PART_ColorShadeSelector = "PART_ColorShadeSelector";
    private const string PART_SpectrumSlider = "PART_SpectrumSlider";
    private const string PART_HexadecimalTextBox = "PART_HexadecimalTextBox";

    public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(nameof(SelectedColor),
        typeof(Color?), typeof(ColorCanvas),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnSelectedColorChanged));

    public static readonly DependencyProperty AProperty = DependencyProperty.Register(nameof(A), typeof(byte),
        typeof(ColorCanvas), new UIPropertyMetadata((byte)255, OnAChanged));

    public static readonly DependencyProperty RProperty = DependencyProperty.Register(nameof(R), typeof(byte),
        typeof(ColorCanvas), new UIPropertyMetadata((byte)0, OnRChanged));

    public static readonly DependencyProperty GProperty = DependencyProperty.Register(nameof(G), typeof(byte),
        typeof(ColorCanvas), new UIPropertyMetadata((byte)0, OnGChanged));

    public static readonly DependencyProperty BProperty = DependencyProperty.Register(nameof(B), typeof(byte),
        typeof(ColorCanvas), new UIPropertyMetadata((byte)0, OnBChanged));

    public static readonly DependencyProperty HexadecimalStringProperty =
        DependencyProperty.Register(nameof(HexadecimalString), typeof(string), typeof(ColorCanvas),
            new UIPropertyMetadata("", OnHexadecimalStringChanged, OnCoerceHexadecimalString));

    public static readonly DependencyProperty UsingAlphaChannelProperty =
        DependencyProperty.Register(nameof(UsingAlphaChannel), typeof(bool), typeof(ColorCanvas),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnUsingAlphaChannelPropertyChanged));

    public static readonly RoutedEvent SelectedColorChangedEvent =
        EventManager.RegisterRoutedEvent("SelectedColorChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<Color?>), typeof(ColorCanvas));

    public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register(
        nameof(HeaderContent),
        typeof(string), 
        typeof(ColorCanvas), 
        new PropertyMetadata(""));

    private Canvas _colorShadeSelector;

    private TranslateTransform _colorShadeSelectorTransform = new TranslateTransform();
    private Canvas _colorShadingCanvas;
    private Point? _currentColorPosition;
    private TextBox _hexadecimalTextBox;
    private ColorSpectrumSlider _spectrumSlider;
    private bool _surpressPropertyChanged;
    private bool _updateSpectrumSliderValue = true;

    static ColorCanvas()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorCanvas),
            new FrameworkPropertyMetadata(typeof(ColorCanvas)));
    }

    public ColorCanvas()
    {
        _spectrumSlider = new ColorSpectrumSlider();
        _colorShadingCanvas = new Canvas();
        _hexadecimalTextBox = new TextBox();
        _colorShadeSelector = new Canvas();
        _colorShadeSelectorTransform = new TranslateTransform();
        _currentColorPosition = new Point();
    }

    public string? HeaderContent
    {
        get => (string?)GetValue(HeaderContentProperty);
        set => SetValue(HeaderContentProperty, value);
    }

    public Color? SelectedColor
    {
        get => (Color?)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    public byte A
    {
        get => (byte)GetValue(AProperty);
        set => SetValue(AProperty, value);
    }

    public byte R
    {
        get => (byte)GetValue(RProperty);
        set => SetValue(RProperty, value);
    }

    public byte G
    {
        get => (byte)GetValue(GProperty);
        set => SetValue(GProperty, value);
    }

    public byte B
    {
        get => (byte)GetValue(BProperty);
        set => SetValue(BProperty, value);
    }

    public string HexadecimalString
    {
        get => (string)GetValue(HexadecimalStringProperty);
        set => SetValue(HexadecimalStringProperty, value);
    }

    public bool UsingAlphaChannel
    {
        get => (bool)GetValue(UsingAlphaChannelProperty);
        set => SetValue(UsingAlphaChannelProperty, value);
    }

    private static void OnSelectedColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        if (o is ColorCanvas colorCanvas)
            colorCanvas.OnSelectedColorChanged((Color?)e.OldValue, (Color?)e.NewValue);
    }

    protected virtual void OnSelectedColorChanged(Color? oldValue, Color? newValue)
    {
        SetHexadecimalStringProperty(GetFormatedColorString(newValue), false);
        UpdateRGBValues(newValue);
        UpdateColorShadeSelectorPosition(newValue);

        var args = new RoutedPropertyChangedEventArgs<Color?>(oldValue, newValue)
        {
            RoutedEvent = SelectedColorChangedEvent
        };
        RaiseEvent(args);
    }

    private static void OnAChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        if (o is ColorCanvas colorCanvas)
            colorCanvas.OnAChanged((byte)e.OldValue, (byte)e.NewValue);
    }

    protected virtual void OnAChanged(byte oldValue, byte newValue)
    {
        if (!_surpressPropertyChanged)
            UpdateSelectedColor();
    }

    private static void OnRChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        if (o is ColorCanvas colorCanvas)
            colorCanvas.OnRChanged((byte)e.OldValue, (byte)e.NewValue);
    }

    protected virtual void OnRChanged(byte oldValue, byte newValue)
    {
        if (!_surpressPropertyChanged)
            UpdateSelectedColor();
    }

    private static void OnGChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        if (o is ColorCanvas colorCanvas)
            colorCanvas.OnGChanged((byte)e.OldValue, (byte)e.NewValue);
    }

    protected virtual void OnGChanged(byte oldValue, byte newValue)
    {
        if (!_surpressPropertyChanged)
            UpdateSelectedColor();
    }

    private static void OnBChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        if (o is ColorCanvas colorCanvas)
            colorCanvas.OnBChanged((byte)e.OldValue, (byte)e.NewValue);
    }

    protected virtual void OnBChanged(byte oldValue, byte newValue)
    {
        if (!_surpressPropertyChanged)
            UpdateSelectedColor();
    }

    private static void OnHexadecimalStringChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        var colorCanvas = o as ColorCanvas;
        if (colorCanvas != null)
            colorCanvas.OnHexadecimalStringChanged((string)e.OldValue, (string)e.NewValue);
    }

    protected virtual void OnHexadecimalStringChanged(string oldValue, string newValue)
    {
        var newColorString = GetFormatedColorString(newValue);
        var currentColorString = GetFormatedColorString(SelectedColor);
        if (!currentColorString.Equals(newColorString))
        {
            Color? col = null;
            if (!string.IsNullOrEmpty(newColorString)) col = (Color)ColorConverter.ConvertFromString(newColorString);
            UpdateSelectedColor(col);
        }

        SetHexadecimalTextBoxTextProperty(newValue);
    }

    private static object OnCoerceHexadecimalString(DependencyObject d, object basevalue)
    {
        var colorCanvas = (ColorCanvas)d;

        return colorCanvas.OnCoerceHexadecimalString(basevalue);
    }

    private object OnCoerceHexadecimalString(object newValue)
    {
        var value = newValue as string;
        var retValue = value;

        try
        {
            if (!string.IsNullOrEmpty(retValue))
            {
                int outValue;
                // User has entered an hexadecimal value (without the "#" character)... add it.
                if (int.TryParse(retValue, NumberStyles.HexNumber, null, out outValue)) retValue = "#" + retValue;
                ColorConverter.ConvertFromString(retValue);
            }
        }
        catch
        {
            //When HexadecimalString is changed via Code-Behind and hexadecimal format is bad, throw.
            throw new InvalidDataException("Color provided is not in the correct format.");
        }

        return retValue;
    }

    private static void OnUsingAlphaChannelPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        var colorCanvas = o as ColorCanvas;
        if (colorCanvas != null)
            colorCanvas.OnUsingAlphaChannelChanged();
    }

    protected virtual void OnUsingAlphaChannelChanged()
    {
        SetHexadecimalStringProperty(GetFormatedColorString(SelectedColor), false);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _colorShadingCanvas.MouseLeftButtonDown -= ColorShadingCanvas_MouseLeftButtonDown;
        _colorShadingCanvas.MouseLeftButtonUp -= ColorShadingCanvas_MouseLeftButtonUp;
        _colorShadingCanvas.MouseMove -= ColorShadingCanvas_MouseMove;
        _colorShadingCanvas.SizeChanged -= ColorShadingCanvas_SizeChanged;

        _colorShadingCanvas = GetTemplateChild(PART_ColorShadingCanvas) as Canvas ?? new Canvas();

        _colorShadingCanvas.MouseLeftButtonDown += ColorShadingCanvas_MouseLeftButtonDown;
        _colorShadingCanvas.MouseLeftButtonUp += ColorShadingCanvas_MouseLeftButtonUp;
        _colorShadingCanvas.MouseMove += ColorShadingCanvas_MouseMove;
        _colorShadingCanvas.SizeChanged += ColorShadingCanvas_SizeChanged;

        _colorShadeSelector = GetTemplateChild(PART_ColorShadeSelector) as Canvas ?? new Canvas();

            _colorShadeSelector.RenderTransform = _colorShadeSelectorTransform;

            _spectrumSlider.ValueChanged -= SpectrumSlider_ValueChanged;

        _spectrumSlider = GetTemplateChild(PART_SpectrumSlider) as ColorSpectrumSlider ?? new ColorSpectrumSlider();

            _spectrumSlider.ValueChanged += SpectrumSlider_ValueChanged;

            _hexadecimalTextBox.LostFocus -= HexadecimalTextBox_LostFocus;

        _hexadecimalTextBox = GetTemplateChild(PART_HexadecimalTextBox) as TextBox ?? new TextBox();

            _hexadecimalTextBox.LostFocus += HexadecimalTextBox_LostFocus;

        UpdateRGBValues(SelectedColor);
        UpdateColorShadeSelectorPosition(SelectedColor);

        // When changing theme, HexadecimalString needs to be set since it is not binded.
        SetHexadecimalTextBoxTextProperty(GetFormatedColorString(SelectedColor));
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        //hitting enter on textbox will update Hexadecimal string
        if (e.Key != Key.Enter || e.OriginalSource is not TextBox textBox) return;
        if (textBox.Name == PART_HexadecimalTextBox)
            SetHexadecimalStringProperty(textBox.Text, true);
    }

    private void ColorShadingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        {
            var p = e.GetPosition(_colorShadingCanvas);
            UpdateColorShadeSelectorPositionAndCalculateColor(p, true);
            _colorShadingCanvas.CaptureMouse();
            //Prevent from closing ColorCanvas after mouseDown in ListView
            e.Handled = true;
        }
    }

    private void ColorShadingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _colorShadingCanvas.ReleaseMouseCapture();
    }

    private void ColorShadingCanvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed) return;
        var p = e.GetPosition(_colorShadingCanvas);
        UpdateColorShadeSelectorPositionAndCalculateColor(p, true);
        Mouse.Synchronize();
    }

    private void ColorShadingCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_currentColorPosition != null)
        {
            var _newPoint = new Point
            {
                X = ((Point)_currentColorPosition).X * e.NewSize.Width,
                Y = ((Point)_currentColorPosition).Y * e.NewSize.Height
            };

            UpdateColorShadeSelectorPositionAndCalculateColor(_newPoint, false);
        }
    }

    private void SpectrumSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (_currentColorPosition != null && SelectedColor != null) CalculateColor((Point)_currentColorPosition);
    }

    private void HexadecimalTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        var textbox = sender as TextBox;
        SetHexadecimalStringProperty(textbox.Text, true);
    }

    public event RoutedPropertyChangedEventHandler<Color?> SelectedColorChanged
    {
        add => AddHandler(SelectedColorChangedEvent, value);
        remove => RemoveHandler(SelectedColorChangedEvent, value);
    }

    private void UpdateSelectedColor()
    {
        SelectedColor = Color.FromArgb(A, R, G, B);
    }

    private void UpdateSelectedColor(Color? color)
    {
        SelectedColor = color != null && color.HasValue
            ? Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B)
            : null;
    }

    private void UpdateRGBValues(Color? color)
    {
        if (color == null)
            return;

        _surpressPropertyChanged = true;

        A = color.Value.A;
        R = color.Value.R;
        G = color.Value.G;
        B = color.Value.B;

        _surpressPropertyChanged = false;
    }

    private void UpdateColorShadeSelectorPositionAndCalculateColor(Point p, bool calculateColor)
    {
        if (p.Y < 0)
            p.Y = 0;

        if (p.X < 0)
            p.X = 0;

        if (p.X > _colorShadingCanvas.ActualWidth)
            p.X = _colorShadingCanvas.ActualWidth;

        if (p.Y > _colorShadingCanvas.ActualHeight)
            p.Y = _colorShadingCanvas.ActualHeight;

        _colorShadeSelectorTransform.X = p.X - _colorShadeSelector.Width / 2;
        _colorShadeSelectorTransform.Y = p.Y - _colorShadeSelector.Height / 2;

        p.X = p.X / _colorShadingCanvas.ActualWidth;
        p.Y = p.Y / _colorShadingCanvas.ActualHeight;

        _currentColorPosition = p;

        if (calculateColor)
            CalculateColor(p);
    }

    private void UpdateColorShadeSelectorPosition(Color? color)
    {
        if (color == null)
            return;

        _currentColorPosition = null;

        var hsv = ColorUtilities.ConvertRgbToHsv(color.Value.R, color.Value.G, color.Value.B);

        if (_updateSpectrumSliderValue) _spectrumSlider.Value = 360 - hsv.H;

        var p = new Point(hsv.S, 1 - hsv.V);

        _currentColorPosition = p;

        _colorShadeSelectorTransform.X = p.X * _colorShadingCanvas.Width - 5;
        _colorShadeSelectorTransform.Y = p.Y * _colorShadingCanvas.Height - 5;
    }

    private void CalculateColor(Point p)
    {
        var hsv = new HsvColor(360 - _spectrumSlider.Value, 1, 1)
        {
            S = p.X,
            V = 1 - p.Y
        };
        var currentColor = ColorUtilities.ConvertHsvToRgb(hsv.H, hsv.S, hsv.V);
        currentColor.A = A;
        _updateSpectrumSliderValue = false;
        SelectedColor = currentColor;
        _updateSpectrumSliderValue = true;
        SetHexadecimalStringProperty(GetFormatedColorString(SelectedColor), false);
    }

    private string GetFormatedColorString(Color? colorToFormat)
    {
        if (colorToFormat == null)
            return string.Empty;
        return ColorUtilities.FormatColorString(colorToFormat.ToString() ?? string.Empty, UsingAlphaChannel);
    }

    private string GetFormatedColorString(string stringToFormat)
    {
        return ColorUtilities.FormatColorString(stringToFormat, UsingAlphaChannel);
    }

    private void SetHexadecimalStringProperty(string newValue, bool modifyFromUI)
    {
        if (modifyFromUI)
            try
            {
                if (!string.IsNullOrEmpty(newValue))
                {
                    int outValue;
                    // User has entered an hexadecimal value (without the "#" character)... add it.
                    if (int.TryParse(newValue, NumberStyles.HexNumber, null, out outValue)) newValue = "#" + newValue;
                    ColorConverter.ConvertFromString(newValue);
                }

                HexadecimalString = newValue;
            }
            catch
            {
                //When HexadecimalString is changed via UI and hexadecimal format is bad, keep the previous HexadecimalString.
                SetHexadecimalTextBoxTextProperty(HexadecimalString);
            }
        else
            //When HexadecimalString is changed via Code-Behind, hexadecimal format will be evaluated in OnCoerceHexadecimalString()
            HexadecimalString = newValue;
    }

    private void SetHexadecimalTextBoxTextProperty(string newValue)
    {
        _hexadecimalTextBox.Text = newValue;
    }
}