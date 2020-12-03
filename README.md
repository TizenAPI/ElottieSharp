# Lottie for Tizen .NET   [![ElottieSharp](https://img.shields.io/nuget/v/ElottieSharp.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/ElottieSharp/) [![ElottieSharp.Forms](https://img.shields.io/nuget/v/ElottieSharp.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/ElottieSharp.Forms/)
ElottieSharp is a library for Tizen .NET that parses [Adobe After Effects](http://www.adobe.com/products/aftereffects.html) animations exported as json with [Bodymovin](https://github.com/bodymovin/bodymovin) and renders them natively on mobile, TV and wearable.

> ~~**[ElottieSharp 0.9.0-preview](https://www.nuget.org/packages/ElottieSharp/0.9.0-preview) version only works on [Galaxy Watch Active](https://www.samsung.com/global/galaxy/galaxy-watch-active/), which supports `Tizen 4.0.0.3`. (Not [Galaxy Watch](https://www.samsung.com/global/galaxy/galaxy-watch/) and [Gear S-series](https://www.samsung.com/global/galaxy/gear-s3/) yet.)**~~

> As the platform version of [Galaxy Watch](https://www.samsung.com/global/galaxy/galaxy-watch/) and [Gear S-series](https://www.samsung.com/global/galaxy/gear-s3/) has been upgraded to version 4.0.0.4, [ElottieSharp 0.9.0-preview](https://www.nuget.org/packages/ElottieSharp/0.9.0-preview) is now cow compatible with  [Galaxy Watch Active](https://www.samsung.com/global/galaxy/galaxy-watch-active/), [Galaxy Watch](https://www.samsung.com/global/galaxy/galaxy-watch/) and [Gear S-series](https://www.samsung.com/global/galaxy/gear-s3/). :tada:

> [ElottieSharp 0.9.1-preview](https://www.nuget.org/packages/ElottieSharp/0.9.1-preview) is now support tizen wearable emulator.

> [ElottieSharp.Forms 0.9.3-preview](https://www.nuget.org/packages/ElottieSharp.Forms/0.9.3-preview) is now only support tizen. (not android, iOS support yet)

> [ElottieSharp 0.9.4-preview](https://www.nuget.org/packages/ElottieSharp/0.9.4-preview) and [ElottieSharp.Forms 0.9.4-preview](https://www.nuget.org/packages/ElottieSharp.Forms/0.9.4-preview) are now support [Galaxy Watch Active2](https://www.samsung.com/global/galaxy/galaxy-watch-active2/).

## Getting Started for *ElottieSharp*
### Installing package 
#### nuget.exe
```
nuget.exe install ElottieSharp -Version 0.9.7-preview2
```
#### .csproj
```xml
<PackageReference Include="ElottieSharp" Version="0.9.7-preview2" />
```
 
### Quick Start
ElottieSharp support Tizen 4.0 (tizen40) and above. 
The simplest way to use it is with LottieAnimationView:
```cs
// Create the LottieAnimationView
var animation = new new LottieAnimationView(window)
{
    AlignmentX = -1,
    AlignmentY = -1,
    WeightX = 1,
    WeightY = 1,
    AutoPlay = true,
};
animation.Show();

// Loading the animation file from a file path
animation.SetAnimation(Path.Combine(DirectoryInfo.Resource, "lottie.json"));

// Play the animation
animation.Play();
```

### LottieAnimationView
`LottieAnimationView` is a `EvasObject (TizenFX)` subclass that displays animation content.

#
#### Creating Animation Views
```cs
var animation = new LottieAnimationView(window)
{
    AutoPlay = true,
    AutoRepeat = false,
    Speed = 1.0,
    MinumumProgress = 0,
    MaximumProgress = 1.0
};
```
Animation views can be allocated with or without animation data. There are a handful of convenience initializers for initializing with animations. 

Properties:
- **AutoPlay**: Indicates whether this animation is play automatially or not.  The default value is `false`.
- **AutoRepeat**: The loop behavior of the animation. The default value is `false`.
- **Speed**: The speed of the animation playback. Higher speed equals faster time. The default value is `1.0`.
- **MinumumProgress**: The start progress of the animation (0 ~ 1.0). The default value is `0`.
- **MaximumProgress**: The end progress of the animation (0 ~ 1.0). The default value is `1.0`.

#
#### Loading from a File Path
```cs
animation.SetAnimation(Path.Combine(DirectoryInfo.Resource, "lottie.json"));
```
Loads an animation model from a filepath. After loading a animation successfully, you can get the duration time and total frame count by using `TotalFrame` and `DurationTime` properties.

Parameters:
: **filepath**: The absolute filepath of the animation to load.


#
#### Play the Animation
```cs
animation.Play();
```
Plays the animation from its current state to the end of its timeline. `Started` event occurs when the animation is started. And `Finished` event occurs when the animation is finished.

#
#### Stop the Animation
```cs
animation.Stop();
```
Stops the animation. `Stopped` event occurs when the animation is stopped.

#
#### Pause the Animation
```cs
animation.Pause();
```
Pauses the animation. `Paused` event occurs when the animation is paused.

#
#### Is Animation Playing
```cs
bool isPlaying = animation.IsPlaying;
```
Returns `true` if the animation is currently playing, `false` if it is not.

#
#### Current Frame
```cs
int currentFrame = animation.CurrentFrame;
```
Returns the current animation frame count. 
==Note==: It returns -1, if animation is not playing.

#
#### Total Frame
```cs
int totalFrame = animation.TotalFrame;
```
Returns the total animation frame count. 
==Note==: You should load the animation file before using it.

#
#### Duration Time
```cs
double duration = animation.DurationTime;
```
Returns the duration time of animation. 
==Note==: You should load the animation file before using it.


## Getting Started for *ElottieSharp.Forms*

### Installing package 
#### nuget.exe
```
nuget.exe install ElottieSharp.Forms -Version 0.9.7-preview2
```
#### .csproj
```xml
<PackageReference Include="ElottieSharp.Forms" Version="0.9.7-preview2" />
```
 
### Quick Start
ElottieSharp.Forms is now only compatible with Tizen (not iOS and andorid yet). 
The simplest way to use it is with ELottieAnimationView:
- C#
```cs
// Create the ELottieAnimationView
var animation = new ELottieAnimationView
{
    AnimationFile = "lottie.json",
    HorizontalOptions = LayoutOptions.FillAndExpand,
    VerticalOptions = LayoutOptions.FillAndExpand
};

// Play the animation
animation.Play();
```

- XAML
```xml
<!-- Create the ELottieAnimationView -->
<e:ElottieAnimationView
    AnimationFile = "lottie.json"
    AutoPlay = "True"
    HorizontalOptions="FillAndExpand"
    VerticalOptions="FillAndExpand"
/>
```

### ELottieAnimationView
`ElottieAnimationView` is a `View (Xamarin.Forms)` subclass that displays animation content.

#
#### Creating Animation Views
- C#
```cs
var animation = new ELottieAnimationView
{
    AutoPlay = true,
    AutoRepeat = false,
    Speed = 1.0,
    MinumumProgress = 0,
    MaximumProgress = 1.0,
    HorizontalOptions = LayoutOptions.FillAndExpand,
    VerticalOptions = LayoutOptions.FillAndExpand
};
```
- XAML
```xml
<e:ElottieAnimationView
    AutoPlay="True"
    AutoRepeat="False"
    Speed="1.0"
    MinumumProgress="0"
    MaximumProgress="1.0"
    HorizontalOptions="FillAndExpand"
    VerticalOptions="FillAndExpand"
/>
```
Animation views can be allocated with or without animation data. There are a handful of convenience initializers for initializing with animations. 

Properties:
- **AutoPlay**: Indicates whether this animation is play automatially or not.  The default value is `false`.
- **AutoRepeat**: The loop behavior of the animation. The default value is `false`.
- **Speed**: The speed of the animation playback. Higher speed equals faster time. The default value is `1.0`.
- **MinumumProgress**: The start progress of the animation (0 ~ 1.0). The default value is `0`.
- **MaximumProgress**: The end progress of the animation (0 ~ 1.0). The default value is `1.0`.

>  By default. the initial value of animation view's size(width, height) is 0. Make sure that using whether `WidthRequest`. `HeightRequest` properties to request the size of animation view.

#
#### Loading from a File Path
- C#
```cs
animation.AnimationFile = "lottie.json";
```
- XAML
```xml
<e:ElottieAnimationView  AnimationFile="lottie.json" />
```
Loads an animation model from a animation file. After loading a animation successfully, you can receive `AnimationInitialized` event and get the duration time and total frame count by using `TotalFrame` and `DurationTime` properties.

#
#### Play the Animation
```cs
animation.Play();
```
Plays the animation from its current state to the end of its timeline. `Started` event occurs when the animation is started. And `Finished` event occurs when the animation is finished.

#
#### Stop the Animation
```cs
animation.Stop();
```
Stops the animation. `Stopped` event occurs when the animation is stopped.

#
#### Pause the Animation
```cs
animation.Pause();
```
Pauses the animation. `Paused` event occurs when the animation is paused.

#
#### Is Animation Playing
```cs
bool isPlaying = animation.IsPlaying;
```
Returns `true` if the animation is currently playing, `false` if it is not.

#
#### Current Frame
```cs
int currentFrame = animation.CurrentFrame;
```
Returns the current animation frame count. 
==Note==: It returns 0, if animation is not playing.

#
#### Total Frame
```cs
int totalFrame = animation.TotalFrame;
```
Returns the total animation frame count. 
==Note==: You should load the animation file before using it.

#
#### Duration Time
```cs
double duration = animation.DurationTime;
```
Returns the duration time of animation. 
==Note==: You should load the animation file before using it.
