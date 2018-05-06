//
//  CryptAES.h
//  demo
//
//  Created by 郭冰冰 on 2018/5/6.
//  Copyright © 2018年 郭冰冰. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CryptAES : NSObject

+(NSData *)AES256ParmEncryptWithKey:(NSString *)key Encrypttext:(NSData *)text;   //加密

+(NSData *)AES256ParmDecryptWithKey:(NSString *)key Decrypttext:(NSData *)text;   //解密

+(NSString *) aes256_encrypt:(NSString *)key Encrypttext:(NSString *)text;

+(NSString *) aes256_decrypt:(NSString *)key Decrypttext:(NSString *)text;

@end
