<?php

/**
 * Project: YunZhangOP
 * File: CryptAES7.php
 * User: guobingbing
 * Time: 2018/6/20 10:39
 * Desc: PHP7实现AES加密
 */

class CryptAES
{
    private static $secret_key;

    public static function set_key($key)
    {
        self::$secret_key = $key;
    }

    public static function encrypt($input)
    {
        $data = openssl_encrypt($input, 'AES-128-ECB', self::$secret_key, OPENSSL_RAW_DATA);
        $data = base64_encode($data);
        return $data;
    }

    public static function decrypt($sStr)
    {
        $decrypted = openssl_decrypt(base64_decode($sStr), 'AES-128-ECB', self::$secret_key, OPENSSL_RAW_DATA);
        return $decrypted;
    }
}

/**
 * 演示
 */
$str = '我真的是一个好人';
CryptAES::set_key('cly123zhang58s@d');
$encode = CryptAES::encrypt($str);
$decode = CryptAES::decrypt($encode);
echo $encode . '<hr />' . $decode;
