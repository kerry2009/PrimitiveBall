//
//  PopupAlert.h
//  IOSUnityPlugin
//
#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>

extern "C" {
    void popupAlert(const char *message);
}