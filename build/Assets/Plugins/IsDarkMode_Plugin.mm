#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

extern "C" {
    bool _IsDakModeSet() {
        if (@available(iOS 12.0, *)) {
            if(UIScreen.mainScreen.traitCollection.userInterfaceStyle == UIUserInterfaceStyleDark) {
                return true;
            }
        }
        
        return false;
    }
}