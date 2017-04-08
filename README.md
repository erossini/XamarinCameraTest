# XamarinCameraTest
Test project for PlugIn.Media

If you execute the code and you try to take some pictures, you can see the problem after 20 pictures. The number of pictures depend from the device:

- iPad Air: 32 pictures
- iPhone 6s: 86 pictures
- iPad Pro 9": 34 pictures

## Bug?

- Version Number of Plugin: Plugin.Media 2.6.2
- Device Tested On: iPadAir, iPhone 6s, iPad Pro

### Expected Behavior

Take unlimited number of pictures

### Actual Behavior

After taking some pictures, the app crashes and no error is reported in the debugger.

No output is stored, or processes or allocated in any way. Still, the app crashes and the debug sessions closes without any messages after taking 30 pictures. At this stage I am confident that the issue lies within the plugin for this platform.

## Update
- First update: 8 April 2017
