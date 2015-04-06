//
//  PopupAlert.m
//  IOSUnityPlugin
//
//  Created by 杨威 on 4/6/15.
//  Copyright (c) 2015 KerryYang. All rights reserved.
//

#import "PopupAlert.h"

void popupAlert(const char *message) {
    NSString *msg = [NSString stringWithUTF8String:message];
    
    UIAlertView* alert;
    alert = [[UIAlertView alloc] initWithTitle:@"Info" message:msg delegate:nil cancelButtonTitle:@"OK" otherButtonTitles: nil];
    [alert show];
}