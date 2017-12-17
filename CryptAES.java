package com.huangpugroup.services.tools;

import com.huangpugroup.services.Comm;
import java.security.Key;
import javax.crypto.Cipher;
import javax.crypto.IllegalBlockSizeException;
import javax.crypto.spec.SecretKeySpec;
import org.apache.commons.codec.binary.Base64;

/**
 * 兼容PHP的AES加密类
 *
 * @author 郭冰冰
 */
public class CryptAES {

    private static final String AESTYPE = "AES/ECB/PKCS5Padding";
    private static final Class OBJ = CryptAES.class;

    /**
     * AES加密
     *
     * @param keyStr 密钥
     * @param plainText 待加密字符串
     * @return 加密后的字符串
     */
    public static String AES_Encrypt(String keyStr, String plainText) {
        byte[] encrypt = null;
        try {
            Key key = generateKey(keyStr);
            Cipher cipher = Cipher.getInstance(AESTYPE);
            cipher.init(Cipher.ENCRYPT_MODE, key);
            encrypt = cipher.doFinal(plainText.getBytes());
        } catch (Exception e) {
            Comm.throwErrorLog(OBJ, "AES加密失败！", e);
        }
        return new String(Base64.encodeBase64(encrypt));
    }

    /**
     * AES解密
     *
     * @param keyStr 密钥
     * @param encryptData 待解密字符串
     * @return 解密后的字符串
     */
    public static String AES_Decrypt(String keyStr, String encryptData) throws IllegalBlockSizeException {
        byte[] decrypt = null;
        try {
            Key key = generateKey(keyStr);
            Cipher cipher = Cipher.getInstance(AESTYPE);
            cipher.init(Cipher.DECRYPT_MODE, key);
            decrypt = cipher.doFinal(Base64.decodeBase64(encryptData));
        } catch (Exception e) {
            Comm.throwErrorLog(OBJ, "AES解密失败！", e);
        }
        return new String(decrypt).trim();
    }

    /**
     * 构造一个密钥
     *
     * @param key 密钥
     * @return
     * @throws Exception
     */
    private static Key generateKey(String key) throws Exception {
        try {
            SecretKeySpec keySpec = new SecretKeySpec(key.getBytes(), "AES");
            return keySpec;
        } catch (Exception e) {
            Comm.throwErrorLog(OBJ, "AES构造密钥失败！", e);
            throw e;
        }

    }

    public static void main(String[] args) throws IllegalBlockSizeException {
        String keyStr = "cly123zhang58s@d";
        String plainText = "我真的是一个好人";
        String encText = AES_Encrypt(keyStr, plainText);
        String decString = AES_Decrypt(keyStr, encText);
        System.out.println(encText);
        System.out.println(decString);
    }
}
