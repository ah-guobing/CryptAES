//
//  CryptAES.m
//  demo
//
//  Created by 郭冰冰 on 2018/5/6.
//  Copyright © 2018年 郭冰冰. All rights reserved.
//

#import "CryptAES.h"
#import <CommonCrypto/CommonCryptor.h>

@implementation CryptAES

+(NSData *)AES256ParmEncryptWithKey:(NSString *)key Encrypttext:(NSData *)text  //加密
{
    char keyPtr[kCCKeySizeAES256+1];
    bzero(keyPtr, sizeof(keyPtr));
    [key getCString:keyPtr maxLength:sizeof(keyPtr) encoding:NSUTF8StringEncoding];
    NSUInteger dataLength = [text length];
    size_t bufferSize = dataLength + kCCBlockSizeAES128;
    void *buffer = malloc(bufferSize);
    size_t numBytesEncrypted = 0;
    CCCryptorStatus cryptStatus = CCCrypt(kCCEncrypt, kCCAlgorithmAES128,
                                          kCCOptionPKCS7Padding | kCCOptionECBMode,
                                          keyPtr, kCCBlockSizeAES128,
                                          NULL,
                                          [text bytes], dataLength,
                                          buffer, bufferSize,
                                          &numBytesEncrypted);
    if (cryptStatus == kCCSuccess) {
        return [NSData dataWithBytesNoCopy:buffer length:numBytesEncrypted];
    }
    free(buffer);
    return nil;
}


+ (NSData *)AES256ParmDecryptWithKey:(NSString *)key Decrypttext:(NSData *)text  //解密
{
    char keyPtr[kCCKeySizeAES256+1];
    bzero(keyPtr, sizeof(keyPtr));
    [key getCString:keyPtr maxLength:sizeof(keyPtr) encoding:NSUTF8StringEncoding];
    NSUInteger dataLength = [text length];
    size_t bufferSize = dataLength + kCCBlockSizeAES128;
    void *buffer = malloc(bufferSize);
    size_t numBytesDecrypted = 0;
    CCCryptorStatus cryptStatus = CCCrypt(kCCDecrypt, kCCAlgorithmAES128,
                                          kCCOptionPKCS7Padding | kCCOptionECBMode,
                                          keyPtr, kCCBlockSizeAES128,
                                          NULL,
                                          [text bytes], dataLength,
                                          buffer, bufferSize,
                                          &numBytesDecrypted);
    if (cryptStatus == kCCSuccess) {
        return [NSData dataWithBytesNoCopy:buffer length:numBytesDecrypted];
    }
    free(buffer);
    return nil;
}


+(NSString *) aes256_encrypt:(NSString *)key Encrypttext:(NSString *)text
{
    const char *cstr = [text cStringUsingEncoding:NSUTF8StringEncoding];
    NSData *data = [NSData dataWithBytes:cstr length:text.length];
    NSData *result = [CryptAES AES256ParmEncryptWithKey:key Encrypttext:data];
    NSString *base64Encoded = [result base64EncodedStringWithOptions:0];
    return base64Encoded;
}


+(NSString *) aes256_decrypt:(NSString *)key Decrypttext:(NSString *)text
{
    NSData *nsdataFromBase64String = [[NSData alloc]
                                      initWithBase64EncodedString:text options:0];
    NSString *base64Decoded = [[NSString alloc]
                               initWithData:nsdataFromBase64String encoding:NSUTF8StringEncoding];
    NSData* result = [CryptAES  AES256ParmDecryptWithKey:key Decrypttext:nsdataFromBase64String];
    if (result && result.length > 0) {
        return [[NSString alloc] initWithData:result encoding:NSUTF8StringEncoding];
    }
    return nil;
    
}

@end
