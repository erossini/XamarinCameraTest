 # XamarinCameraTest
Test project for taking photos

If you want to replicate the error, I created a test project for you on GitHub. With my iPad Air or iPad Pro after about 30 photos (video [iPad Air](https://youtu.be/od0Qx5YdMo0) - [iPad Pro](https://youtu.be/0IReKcFyPcc)). All devices are iOS ver. 10.3.1 and they have enough space to storage photos.

If you execute the code and you try to take some pictures, you can see the problem after 20 pictures. The number of pictures depend from the device:

- iPad Air 32
- iPhone 6s 86
- iPad Pro 9" 34

Generate and deploy with `Xcode` ver. 8.3.1 (8E1000a) on:

- MacBook Air
- iMac
- MacBook Pro

## Updates
- First update: 8 April 2017
- 10 April: add new project `cameratesteasy` without `Refractored.MvvmHelpers` and `Media.Plugin`
- 11 April: add code for taking photo with native code. In this case I receive alert from `ReceiveMemoryWarning`. Open a [bug](https://bugzilla.xamarin.com/show_bug.cgi?id=55010).

## Bug 

- Version Number of Plugin: Plugin.Media 2.6.2
- Device Tested On: iPadAir, iPhone 6s, iPad Pro

### Expected Behavior
Take unlimited number of pictures

### Actual Behavior
After taking some pictures, the app crashes and no error is reported in the debugger. You can see my test code https://github.com/erossini/XamarinCameraTest

No output is stored, or processes or allocated in any way. Still, the app crashes and the debug sessions closes without any messages after taking 30 pictures. At this stage I am confident that the issue lies within the plugin for this platform.

I tried to check the code adding `Media.Plugin`, `Media.Plugin.Abstractions`, `Media.Plugin.iOS` and `Media.Plugin.Shared` in my solution but there is an error:

![screen shot 2017-04-08 at 22 56 31](https://cloud.githubusercontent.com/assets/9497415/24832763/dba682ba-1cae-11e7-98a0-31774d958eb7.png)

### Profiler
If I try to use `Xamarin Profiler` is also crashed with this error:

![screen shot 2017-04-09 at 01 20 07](https://cloud.githubusercontent.com/assets/9497415/24833511/e199c4f2-1cc2-11e7-97e3-96a3f1dcb53c.png)
