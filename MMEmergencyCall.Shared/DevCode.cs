﻿using Effortless.Net.Encryption;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMEmergencyCall.Shared;

public static class DevCode
{
    private static byte[] key = Convert.FromBase64String("pY3RDfDlt7xwAnKVu/MhiQxM5u//oe4jt9L+rhyECSY=");
    private static byte[] iv = Convert.FromBase64String("C76NDuPRRtP20EMiYMyHTQ==");

    public static string ToEncrypt(this string plainText) // plain text to cipher text
    {
        string encrypted = Strings.Encrypt(plainText, key, iv);
        return encrypted;
    }

    public static string ToDecrypt(this string encrypted) // cipher text to plain text
    {
        string decrypted = Strings.Decrypt(encrypted, key, iv);
        return decrypted;
    }

    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T ToObject<T>(this string str)
    {
        return JsonConvert.DeserializeObject<T>(str)!;
    }
}