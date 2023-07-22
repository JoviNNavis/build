# BrightData SDK integration guide

This package allows to integrate BrightData SDK into your apps.

## Documentation

Full integration guide is provided online - [https://docs.google.com/document/d/1uK0PE5NRqdF9V1S4I19xAmlFeoXTuFIQh3m5VvmiQWs/view#heading=h.5f0ba8e5hwx2](https://docs.google.com/document/d/1uK0PE5NRqdF9V1S4I19xAmlFeoXTuFIQh3m5VvmiQWs/view#heading=h.5f0ba8e5hwx2)

This document partially duplicates the online documentation.

## Prerequisites
- Xcode 13.4 or higher
- Unity 2020.2.5f1 or higher

## Structure of the source code

- `BrightDataSDK` - the main folder with SDK files;
- `BrightDataSDK/brdsdk.framework` - the compiled iOS framework for real
   devices (not working in iOS simulators);
- `BrightDataSDK/Sample` - the folder contains an example of how you can 
   integrate the SDK in your code;
- `BrightDataSDK/Sample/Scenes/` - the folder contains sample scene 
   `BrdsdkSampleScene` showing how you can integrate the SDK (it just shows 
   the correct flow on how the SDK is used in the code). 

## Sample Scene

You can use the sample scene to check on how the SDK works before integrating
it in your code.

- Open `BrdsdkSampleScene` from `BrightDataSDK/Sample/Scenes/.
- Make sure you will include the scene when Xcode project is generated
  on the next step.
- Generate Xcode project and run the app.
  Follow "Step 2.1.2. Generate Xcode project" section in
  ["iOS SDK Integration Guide for Unity"](https://docs.google.com/document/d/1uK0PE5NRqdF9V1S4I19xAmlFeoXTuFIQh3m5VvmiQWs/view)
  to generate Xcode project and run the app.

## SDK integration

1. Open `BrightDataSDK/Sample/SampleBehaviour.cs` and get familiar with the 
sample code. Add `BrdsdkBridge.init(..)` call into your code.
2. Open `BrightDataSDK/Sample/SettingsBehaviour.cs` and verify on how to use
other API methods in you Settings screen:
- `BrdsdkBridge.get_choice()`;
- `BrdsdkBridge.show_consent();`
- `BrdsdkBridge.opt_out();`
3. Implement your own Settings screen following the recommendations in 
["Step 2.4: Opt-out option"](https://docs.google.com/document/d/1uK0PE5NRqdF9V1S4I19xAmlFeoXTuFIQh3m5VvmiQWs/view#heading=h.hl9wma7gpjsy)
section.

### API methods

**`BrdsdkBridge.init(...)`**
Initializes the SDK.
Should be called at the app init. All other methods will be accessible after 
successful initialization.
```
BrdsdkBridge.init(
    string benefit_text, 
    string agree_btn, 
    string disagree_btn,
    ChoiceCallback on_choice_callback
)
```
- `benefit_txt` (string) the text which is used in the consent screen as 
a prefix: *"<benefit_txt>, allow Bright Data to use your device’s free resources 
and IP address to download…"*. If the parameter is not provided, then *"To
support <app_name>"* is used by default, i.g. *"To support My Sample App, allow
Bright Data to use your device’s free resources and IP address to download…"*

- `agree_btn` (string) - consent screen "agree" button text. 
Default value: "I Agree".

- `disagree_btn` (string) - consent screen "disagree" button text.
Default value: "I Disagree".

- `on_choice_callback` (function) - callback will be called every time a user
makes a choice or right after the `.init()` method is called. The parameter has 
the same meaning as the result of `BrdsdkBridge.get_choice()` (see it's
documentation for possible returned values).
 
**`BrdsdkBridge.show_consent()`**
Shows the consent screen on the user`s action. This can be used when a user
tries to close an ad or clicks the checkbox in Settings screen to activate 
Bright SDK.

**`BrdsdkBridge.opt_out()`** - disables Bright SDK.

**`BrdsdkBridge.get_choice()`** - returns the user`s choice status.
Possible cases:
- `BrdsdkBridge.CHOICE_AGREED (1)`  - SDK is enabled and works;
- `BrdsdkBridge.CHOICE_DISAGREED (2)` - SDK is disabled and disconnected;
- `BrdsdkBridge.CHOICE_NONE (0)` - before the user passes the consent screen for 
   the first time.
