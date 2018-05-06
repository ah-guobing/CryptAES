//
//  main.m
//  demo
//
//  Created by 郭冰冰 on 2018/5/6.
//  Copyright © 2018年 郭冰冰. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "CryptAES.h"

int main(int argc, const char * argv[]) {
    @autoreleasepool {
        NSString *EC=[CryptAES aes256_encrypt:@"cly123zhang58s@d" Encrypttext:@"abcdefg 123dd"];
        NSLog(@"\rEncode：%@\r",EC);
        NSString *DC=[CryptAES aes256_decrypt:@"cly123zhang58s@d" Decrypttext:EC];
        NSLog(@"\rDecode：%@\r",DC);
    }
    return 0;
}
