---
title: "Custom Keystore Providers"
description: "Learn about how to implement a custom key store provider for use with the ODBC Driver for SQL Server and the Always Encrypted feature."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: "07/12/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Custom Keystore Providers

[!INCLUDE[Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

## Overview

The column encryption feature of SQL Server 2016 requires that the Encrypted Column Encryption Keys (ECEKs) stored on the server are retrieved by the client and then decrypted to Column Encryption Keys (CEKs) in order to access the data stored in encrypted columns. ECEKs are encrypted by Column Master Keys (CMKs), and the security of the CMK is important to the security of column encryption. Thus, the CMK should be stored in a secure location; the purpose of a Column Encryption Keystore Provider is to provide an interface to allow the ODBC driver to access these securely stored CMKs. For users with their own secure storage, the Custom Keystore Provider Interface provides a framework for implementing access to secure storage of the CMK for the ODBC driver, which can then be used to perform CEK encryption and decryption.

Each keystore provider contains and manages one or more CMKs, which are identified by key paths - strings of a format defined by the provider. This CMK, along with the encryption algorithm, also a string defined by the provider, can be used to perform the encryption of a CEK and the decryption of an ECEK. The algorithm, along with the ECEK and the name of the provider, are stored in the database's encryption metadata. For more information, see [CREATE COLUMN MASTER KEY](../../t-sql/statements/create-column-master-key-transact-sql.md) and [CREATE COLUMN ENCRYPTION KEY](../../t-sql/statements/create-column-encryption-key-transact-sql.md). Thus, the two fundamental operations of key management are:

```cpp
CEK = DecryptViaCEKeystoreProvider(CEKeystoreProvider_name, Key_path, Key_algorithm, ECEK)

-and-

ECEK = EncryptViaCEKeystoreProvider(CEKeyStoreProvider_name, Key_path, Key_algorithm, CEK)
```

where the `CEKeystoreProvider_name` is used to identify the specific Column Encryption Keystore Provider (CEKeystoreProvider), and the other arguments are used by the CEKeystoreProvider to encrypt/decrypt the (E)CEK. The name and key path are provided by the CMK metadata, while the algorithm and ECEK value are provided by the CEK metadata. Multiple keystore providers may be present alongside the default built-in provider(s). Upon performing an operation that requires the CEK, the driver uses the CMK metadata to find the appropriate keystore provider by name, and executes its decryption operation, which can be expressed as:

```cpp
CEK = CEKeyStoreProvider_specific_decrypt(Key_path, Key_algorithm, ECEK)
```

Although the driver has no need to encrypt CEKs, a key management tool may need to do so in order to implement operations such as CMK creation and rotation. These actions require performing the inverse operation:

```cpp
ECEK = CEKeyStoreProvider_specific_encrypt(Key_path, Key_algorithm, CEK)
```

### CEKeyStoreProvider interface

This document describes in detail the CEKeyStoreProvider interface. A keystore provider that implements this interface can be used by the Microsoft ODBC Driver for SQL Server. CEKeyStoreProvider implementers can use this guide to develop custom keystore providers usable by the driver.

A keystore provider library ("provider library") is a dynamic-link library that can be loaded by the ODBC driver, and contains one or more keystore providers. The symbol `CEKeystoreProvider` must be exported by a provider library, and be the address of a null-terminated array of pointers to `CEKeystoreProvider` structures, one for each keystore provider within the library.

A `CEKeystoreProvider` structure defines the entry points of a single keystore provider:

```cpp
typedef struct CEKeystoreProvider {
    wchar_t *Name;
    int (*Init)(CEKEYSTORECONTEXT *ctx, errFunc *onError);
    int (*Read)(CEKEYSTORECONTEXT *ctx, errFunc *onError, void *data, unsigned int *len);
    int (*Write)(CEKEYSTORECONTEXT *ctx, errFunc *onError, void *data, unsigned int len);
    int (*DecryptCEK)(  CEKEYSTORECONTEXT *ctx,
                        errFunc *onError,
                        const wchar_t *keyPath,
                        const wchar_t *alg,
                        unsigned char *ecek,
                        unsigned short ecekLen,
                        unsigned char **cekOut,
                        unsigned short *cekLen);
    int (*EncryptCEK)(  CEKEYSTORECONTEXT *ctx,
                        errFunc *onError,
                        const wchar_t *keyPath,
                        const wchar_t *alg,
                        unsigned char *cek,
                        unsigned short cekLen,
                        unsigned char **ecekOut,
                        unsigned short *ecekLen);
    void (*Free)();
} CEKEYSTOREPROVIDER;
```

|Field Name|Description|
|:--|:--|
|`Name`|The name of the keystore provider. It must not be the same as any other keystore provider previously loaded by the driver or present in this library. Null-terminated, wide-character* string.|
|`Init`|Initialization function. If an initialization function is not required, this field may be null.|
|`Read`|Provider read function. May be null if not required.|
|`Write`|Provider write function. Required if Read is not null. May be null if not required.|
|`DecryptCEK`|ECEK decryption function. This function is the reason for existence of a keystore provider, and must not be null.|
|`EncryptCEK`|CEK encryption function. The driver does not call this function, but it is provided to allow for programmatic access to ECEK creation by key management tools. May be null if not required.|
|`Free`|Termination function. May be null if not required.|

Except for Free, the functions in this interface all have a pair of parameters, **ctx** and **onError**. The former identifies the context in which the function is called, while the latter is used for reporting errors. For more information, see [Contexts](#context-association) and [Error Handling](#error-handling) below.

```cpp
int Init(CEKEYSTORECONTEXT *ctx, errFunc onError);
```

Placeholder name for a provider-defined initialization function. The driver calls this function once, after a provider has been loaded, but before the first time it needs it to perform ECEK decryption or Read()/Write() requests. Use this function to perform any initialization it needs.

|Argument|Description|
|:--|:--|
|`ctx`|[Input] Operation context.|
|`onError`|[Input] Error-reporting function.|
|`Return Value`|Return nonzero to indicate success, or zero to indicate failure.|

```cpp
int Read(CEKEYSTORECONTEXT *ctx, errFunc onError, void *data, unsigned int *len);
```

Placeholder name for a provider-defined communication function. The driver calls this function when the application requests to read data from a (previously written to) provider using the SQL_COPT_SS_CEKEYSTOREDATA connection attribute, allowing the application to read arbitrary data from the provider. For more information, see [Communicating with Keystore Providers](using-always-encrypted-with-the-odbc-driver.md#communicating-with-keystore-providers).

|Argument|Description|
|:--|:--|
|`ctx`|[Input] Operation context.|
|`onError`|[Input] Error-reporting function.|
|`data`|[Output] Pointer to a buffer in which the provider writes data to be read by the application. This buffer corresponds to the data field of the CEKEYSTOREDATA structure.|
|`len`|[InOut] Pointer to a length value; upon input, this value is the maximum length of the data buffer, and the provider shall not write more than `*len` bytes to it. Upon return, the provider should update `*len` with the number of bytes written.|
|`Return Value`|Return nonzero to indicate success, or zero to indicate failure.|

```cpp
int Write(CEKEYSTORECONTEXT *ctx, errFunc onError, void *data, unsigned int len);
```

Placeholder name for a provider-defined communication function. The driver calls this function when the application requests to write data to a provider using the SQL_COPT_SS_CEKEYSTOREDATA connection attribute, allowing the application to write arbitrary data to the provider. For more information, see [Communicating with Keystore Providers](using-always-encrypted-with-the-odbc-driver.md#communicating-with-keystore-providers).

|Argument|Description|
|:--|:--|
|`ctx`|[Input] Operation context.|
|`onError`|[Input] Error-reporting function.|
|`data`|[Input] Pointer to a buffer containing the data for the provider to read. This buffer corresponds to the data field of the CEKEYSTOREDATA structure. The provider must not read more than len bytes from this buffer.|
|`len`|[Input] The number of bytes available in data. This value corresponds to the dataSize field of the CEKEYSTOREDATA structure.|
|`Return Value`|Return nonzero to indicate success, or zero to indicate failure.|

```cpp
int (*DecryptCEK)( CEKEYSTORECONTEXT *ctx, errFunc *onError, const wchar_t *keyPath, const wchar_t *alg, unsigned char *ecek, unsigned short ecekLen, unsigned char **cekOut, unsigned short *cekLen);
```

Placeholder name for a provider-defined ECEK decryption function. The driver calls this function to decrypt an ECEK encrypted by a CMK associated with this provider into a CEK.

|Argument|Description|
|:--|:--|
|`ctx`|[Input] Operation context.|
|`onError`|[Input] Error-reporting function.|
|`keyPath`|[Input] The value of the [KEY_PATH](../../t-sql/statements/create-column-master-key-transact-sql.md) metadata attribute for the CMK referenced by the given ECEK. Null-terminated wide-character* string. This value is intended to identify a CMK handled by this provider.|
|`alg`|[Input] The value of the [ALGORITHM](../../t-sql/statements/create-column-encryption-key-transact-sql.md) metadata attribute for the given ECEK. Null-terminated wide-character* string. This value is intended to identify the encryption algorithm used to encrypt the given ECEK.|
|`ecek`|[Input] Pointer to the ECEK to be decrypted.|
|`ecekLen`|[Input] Length of the ECEK.|
|`cekOut`|[Output] The provider shall allocate memory for the decrypted ECEK and write its address to the pointer pointed to by cekOut. It must be possible to free this block of memory using the [LocalFree](/windows/desktop/api/winbase/nf-winbase-localfree) (Windows) or free (Linux/macOS) function. If no memory was allocated due to an error or otherwise, the provider shall set *cekOut to a null pointer.|
|`cekLen`|[Output] The provider shall write to the address pointed to by cekLen the length of the decrypted ECEK that it has written to **cekOut.|
|`Return Value`|Return nonzero to indicate success, or zero to indicate failure.|

```cpp
int (*EncryptCEK)( CEKEYSTORECONTEXT *ctx, errFunc *onError, const wchar_t *keyPath, const wchar_t *alg, unsigned char *cek,unsigned short cekLen, unsigned char **ecekOut, unsigned short *ecekLen);
```

Placeholder name for a provider-defined CEK encryption function. The driver does not call this function nor expose its functionality through the ODBC interface, but it is provided to allow for programmatic access to ECEK creation by key management tools.

|Argument|Description|
|:--|:--|
|`ctx`|[Input] Operation context.|
|`onError`|[Input] Error-reporting function.|
|`keyPath`|[Input] The value of the [KEY_PATH](../../t-sql/statements/create-column-master-key-transact-sql.md) metadata attribute for the CMK referenced by the given ECEK. Null-terminated wide-character* string. This value is intended to identify a CMK handled by this provider.|
|`alg`|[Input] The value of the [ALGORITHM](../../t-sql/statements/create-column-encryption-key-transact-sql.md) metadata attribute for the given ECEK. Null-terminated wide-character* string. This value is intended to identify the encryption algorithm used to encrypt the given ECEK.|
|`cek`|[Input] Pointer to the CEK to be encrypted.|
|`cekLen`|[Input] Length of the CEK.|
|`ecekOut`|[Output] The provider shall allocate memory for the encrypted CEK and write its address to the pointer pointed to by ecekOut. It must be possible to free this block of memory using the [LocalFree](/windows/desktop/api/winbase/nf-winbase-localfree) (Windows) or free (Linux/macOS) function. If no memory was allocated due to an error or otherwise, the provider shall set *ecekOut to a null pointer.|
|`ecekLen`|[Output] The provider shall write to the address pointed to by ecekLen the length of the encrypted CEK that it has written to **ecekOut.|
|`Return Value`|Return nonzero to indicate success, or zero to indicate failure.|

```cpp
void (*Free)();
```

Placeholder name for a provider-defined termination function. The driver may call this function upon normal termination of the process.

> [!NOTE]
> *Wide-character strings are 2-byte characters (UTF-16) due to how SQL Server stores them.*

### Error Handling

As errors may occur during a provider's processing, a mechanism is provided to allow it to report errors back to the driver in more specific detail than a boolean success/failure. Many of the functions have a pair of parameters, **ctx** and **onError**, which are used together for this purpose in addition to the success/failure return value.

The **ctx** parameter identifies the context in which a provider operation occurs.

The **onError** parameter points to an error-reporting function, with the following prototype:

`typedef void errFunc(CEKEYSTORECONTEXT *ctx, const wchar_t *msg, ...);`

|Argument|Description|
|:--|:--|
|`ctx`|[Input] The context to report the error on.|
|`msg`|[Input] The error message to report. Null-terminated wide-character string. To allow parameterized information, this string may contain insert-formatting sequences of the form accepted by the [FormatMessage](/windows/desktop/api/winbase/nf-winbase-formatmessage) function. Extended functionality may be specified by this parameter as described below.|
|...|[Input] Extra variadic parameters to fit format specifiers in msg, as appropriate.|

To report when an error has occurred, the provider calls onError, supplying the context parameter passed into the provider function by the driver and an error message with optional extra parameters to be formatted in it. The provider may call this function multiple times to post multiple error messages consecutively within one provider-function invocation. For example:

```cpp
    if (!doSomething(...))
    {
        onError(ctx, L"An error occurred in doSomething.");
        onError(ctx, L"Additional error message with more details.");
        return 0;
    }
```

The `msg` parameter is ordinarily a wide-character string, but more extensions are available:

By using one of the special predefined values with the IDS_MSG macro, generic error messages already existing and in a localized form in the driver may be utilized. For example, if a provider fails to allocate memory, the `IDS_S1_001` "Memory allocation failure" message can be used:

`onError(ctx, IDS_MSG(IDS_S1_001));`

For the error to be recognized by the driver, the provider function must return failure. When a failure happens in the context of an ODBC operation, the posted errors will become accessible on the connection or statement handle via the standard ODBC diagnostics mechanism (`SQLError`, `SQLGetDiagRec`, and `SQLGetDiagField`).

### Context Association

The `CEKEYSTORECONTEXT` structure, in addition to providing context for the error callback, can also be used to determine the ODBC context in which a provider operation is executed. This context allows a provider to associate data to each of these contexts, for example, to implement per-connection configuration. For this purpose, the structure contains three opaque pointers corresponding to the environment, connection, and statement context:

```cpp
typedef struct CEKeystoreContext
{
void *envCtx;
void *dbcCtx;
void *stmtCtx;
} CEKEYSTORECONTEXT;
```

|Field|Description|
|:--|:--|
|`envCtx`|Environment context.|
|`dbcCtx`|Connection context.|
|`stmtCtx`|Statement context.|

Each of these contexts is an opaque value which, while not the same as the corresponding ODBC handle, can be used as a unique identifier for the handle: if handle *X* is associated with context value *Y*, then no other environment, connection, or statement handles that exist simultaneously at the same time as *X* will have a context value of *Y*, and no other context values will be associated with handle *X*. If the provider operation being accomplished lacks a particular handle context (for example, SQLSetConnectAttr calls to load and configure providers, in which there is no statement handle), the corresponding context value in the structure is null.

## Example

### Keystore Provider

The following code is an example of a minimal keystore provider implementation.

```cpp
/* Custom Keystore Provider Example

Windows:   compile with cl MyKSP.c /LD /MD /link /out:MyKSP.dll
Linux/macOS: compile with gcc -fshort-wchar -fPIC -o MyKSP.so -shared MyKSP.c

 */

#ifdef _WIN32
#include <windows.h>
#else
#define __stdcall
#endif

#include <stdlib.h>
#include <sqltypes.h>
#include "msodbcsql.h"
#include <sql.h>
#include <sqlext.h>

int __stdcall KeystoreInit(CEKEYSTORECONTEXT *ctx, errFunc *onError) {
    printf("KSP Init() function called\n");
    return 1;
}

static unsigned char *g_encryptKey;
static unsigned int g_encryptKeyLen;

int __stdcall KeystoreWrite(CEKEYSTORECONTEXT *ctx, errFunc *onError, void *data, unsigned int len) {
    printf("KSP Write() function called (%d bytes)\n", len);
    if (len) {
        if (g_encryptKey)
            free(g_encryptKey);
        g_encryptKey = malloc(len);
        if (!g_encryptKey) {
            onError(ctx, L"Memory Allocation Error");
            return 0;
        }
        memcpy(g_encryptKey, data, len);
        g_encryptKeyLen = len;
    }
    return 1;
}

// Very simple "encryption" scheme - rotating XOR with the key
int __stdcall KeystoreDecrypt(CEKEYSTORECONTEXT *ctx, errFunc *onError, const wchar_t *keyPath, const wchar_t *alg,
    unsigned char *ecek, unsigned short ecekLen, unsigned char **cekOut, unsigned short *cekLen) {
    unsigned int i;
    printf("KSP Decrypt() function called (keypath=%S alg=%S ecekLen=%u)\n", keyPath, alg, ecekLen);
    if (wcscmp(keyPath, L"TheOneAndOnlyKey")) {
        onError(ctx, L"Invalid key path");
        return 0;
    }
    if (wcscmp(alg, L"none")) {
        onError(ctx, L"Invalid algorithm");
        return 0;
    }
    if (!g_encryptKey) {
        onError(ctx, L"Keystore provider not initialized with key");
        return 0;
    }
#ifndef _WIN32
    *cekOut = malloc(ecekLen);
#else
    *cekOut = LocalAlloc(LMEM_FIXED, ecekLen);
#endif
    if (!*cekOut) {
        onError(ctx, L"Memory Allocation Error");
        return 0;
    }
    *cekLen = ecekLen;
    for (i = 0; i < ecekLen; i++)
        (*cekOut)[i] = ecek[i] ^ g_encryptKey[i % g_encryptKeyLen];
    return 1;
}

// Note that in the provider interface, this function would be referenced via the CEKEYSTOREPROVIDER
// structure. However, that does not preclude keystore providers from exporting their own functions,
// as illustrated by this example where the encryption is performed via a separate function (with a
// different prototype than the one in the KSP interface.)
#ifdef _WIN32
__declspec(dllexport)
#endif
int KeystoreEncrypt(CEKEYSTORECONTEXT *ctx, errFunc *onError,
    unsigned char *cek, unsigned short cekLen,
    unsigned char **ecekOut, unsigned short *ecekLen) {
    unsigned int i;
    printf("KSP Encrypt() function called (cekLen=%u)\n", cekLen);
    if (!g_encryptKey) {
        onError(ctx, L"Keystore provider not initialized with key");
        return 0;
    }
    *ecekOut = malloc(cekLen);
    if (!*ecekOut) {
        onError(ctx, L"Memory Allocation Error");
        return 0;
    }
    *ecekLen = cekLen;
    for (i = 0; i < cekLen; i++)
        (*ecekOut)[i] = cek[i] ^ g_encryptKey[i % g_encryptKeyLen];
    return 1;
}

CEKEYSTOREPROVIDER MyCustomKSPName_desc = {
    L"MyCustomKSPName",
    KeystoreInit,
    0,
    KeystoreWrite,
    KeystoreDecrypt,
    0
};

#ifdef _WIN32
__declspec(dllexport)
#endif
CEKEYSTOREPROVIDER *CEKeystoreProvider[] = {
    &MyCustomKSPName_desc,
    0
};
```

### ODBC Application

The following code is a demo application that uses the keystore provider above. When running it, ensure that the provider library is in the same directory as the application binary, and that the connection string specifies (or specifies a DSN that contains) the `ColumnEncryption=Enabled` setting.

```cpp
/*
 Example application for demonstration of custom keystore provider usage

Windows:   compile with cl /MD kspapp.c /link odbc32.lib
Linux/macOS: compile with gcc -o kspapp -fshort-wchar kspapp.c -lodbc -ldl
 
 usage: kspapp connstr

 */

#define KSPNAME L"MyCustomKSPName"
#define PROV_ENCRYPT_KEY "JHKCWYT06N3RG98J0MBLG4E3"

#include <stdio.h>
#include <stdlib.h>
#ifdef _WIN32
#include <windows.h>
#else
#define __stdcall
#include <dlfcn.h>
#endif
#include <sql.h>
#include <sqlext.h>
#include "msodbcsql.h"

/* Convenience functions */

int checkRC(SQLRETURN rc, char *msg, int ret, SQLHANDLE h, SQLSMALLINT ht) {
    if (rc == SQL_ERROR) {
        fprintf(stderr, "Error occurred upon %s\n", msg);
        if (h) {
            SQLSMALLINT i = 0;
            SQLSMALLINT outlen = 0;
            char errmsg[1024];
            while ((rc = SQLGetDiagField(
                ht, h, ++i, SQL_DIAG_MESSAGE_TEXT, errmsg, sizeof(errmsg), &outlen)) == SQL_SUCCESS
                || rc == SQL_SUCCESS_WITH_INFO) {
                fprintf(stderr, "Err#%d: %s\n", i, errmsg);
            }
        }
        if (ret)
            exit(ret);
        return 0;
    }
    else if (rc == SQL_SUCCESS_WITH_INFO && h) {
        SQLSMALLINT i = 0;
        SQLSMALLINT outlen = 0;
        char errmsg[1024];
        printf("Success with info for %s:\n", msg);
        while ((rc = SQLGetDiagField(
            ht, h, ++i, SQL_DIAG_MESSAGE_TEXT, errmsg, sizeof(errmsg), &outlen)) == SQL_SUCCESS
            || rc == SQL_SUCCESS_WITH_INFO) {
            fprintf(stderr, "Msg#%d: %s\n", i, errmsg);
        }
    }
    return 1;
}

void postKspError(CEKEYSTORECONTEXT *ctx, const wchar_t *msg, ...) {
    if (msg > (wchar_t*)65535)
        wprintf(L"Provider emitted message: %s\n", msg);
    else
        wprintf(L"Provider emitted message ID %d\n", msg);
}

int main(int argc, char **argv) {
    char sqlbuf[1024];
    SQLHENV env;
    SQLHDBC dbc;
    SQLHSTMT stmt;
    SQLRETURN rc;
    unsigned char CEK[32];
    unsigned char *ECEK;
    unsigned short ECEKlen;
#ifdef _WIN32
    HMODULE hProvLib;
#else
    void *hProvLib;
#endif
    CEKEYSTORECONTEXT ctx = {0};
    CEKEYSTOREPROVIDER **ppKsp, *pKsp;
    int(__stdcall *pEncryptCEK)(CEKEYSTORECONTEXT *, errFunc *, unsigned char *, unsigned short, unsigned char **, unsigned short *);
    int i;
    if (argc < 2) {
        fprintf(stderr, "usage: kspapp connstr\n");
        return 1;
    }

    /* Load the provider library */
#ifdef _WIN32
    if (!(hProvLib = LoadLibrary("MyKSP.dll"))) {
#else
    if (!(hProvLib = dlopen("./MyKSP.so", RTLD_NOW))) {
#endif
        fprintf(stderr, "Error loading KSP library\n");
        return 2;
    }
#ifdef _WIN32
    if (!(ppKsp = (CEKEYSTOREPROVIDER**)GetProcAddress(hProvLib, "CEKeystoreProvider"))) {
#else
    if (!(ppKsp = (CEKEYSTOREPROVIDER**)dlsym(hProvLib, "CEKeystoreProvider"))) {
#endif
        fprintf(stderr, "The export CEKeystoreProvider was not found in the KSP library\n");
        return 3;
    }
    while (pKsp = *ppKsp++) {
        if (!memcmp(KSPNAME, pKsp->Name, sizeof(KSPNAME)))
            goto FoundProv;
    }
    fprintf(stderr, "Could not find provider in the library\n");
    return 4;
FoundProv:
    if (pKsp->Init && !pKsp->Init(&ctx, postKspError)) {
        fprintf(stderr, "Could not initialize provider\n");
        return 5;
    }
#ifdef _WIN32
    if (!(pEncryptCEK = (LPVOID)GetProcAddress(hProvLib, "KeystoreEncrypt"))) {
#else
    if (!(pEncryptCEK = dlsym(hProvLib, "KeystoreEncrypt"))) {
#endif
        fprintf(stderr, "The export KeystoreEncrypt was not found in the KSP library\n");
        return 6;
    }
    if (!pKsp->Write) {
        fprintf(stderr, "Provider does not support configuration\n");
        return 7;
    }

    /* Configure the provider with the key */
    if (!pKsp->Write(&ctx, postKspError, PROV_ENCRYPT_KEY, strlen(PROV_ENCRYPT_KEY))) {
        fprintf(stderr, "Error writing to KSP\n");
        return 8;
    }

    /* Generate a CEK and encrypt it with the provider */
    srand(time(0) ^ getpid());
    for (i = 0; i < sizeof(CEK); i++)
        CEK[i] = rand();

    if (!pEncryptCEK(&ctx, postKspError, CEK, sizeof(CEK), &ECEK, &ECEKlen)) {
        fprintf(stderr, "Error encrypting CEK\n");
        return 9;
    }

    /* Connect to Server */
    rc = SQLAllocHandle(SQL_HANDLE_ENV, NULL, &env);
    checkRC(rc, "allocating environment handle", 2, 0, 0);
    rc = SQLSetEnvAttr(env, SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3, 0);
    checkRC(rc, "setting ODBC version to 3.0", 3, env, SQL_HANDLE_ENV);
    rc = SQLAllocHandle(SQL_HANDLE_DBC, env, &dbc);
    checkRC(rc, "allocating connection handle", 4, env, SQL_HANDLE_ENV);
    rc = SQLDriverConnect(dbc, 0, argv[1], strlen(argv[1]), NULL, 0, NULL, SQL_DRIVER_NOPROMPT);
    checkRC(rc, "connecting to data source", 5, dbc, SQL_HANDLE_DBC);
    rc = SQLAllocHandle(SQL_HANDLE_STMT, dbc, &stmt);
    checkRC(rc, "allocating statement handle", 6, dbc, SQL_HANDLE_DBC);

    /* Create a CMK definition on the server */
    {
        static char cmkSql[] = "CREATE COLUMN MASTER KEY CustomCMK WITH ("
            "KEY_STORE_PROVIDER_NAME = 'MyCustomKSPName',"
            "KEY_PATH = 'TheOneAndOnlyKey')";
        printf("Create CMK: %s\n", cmkSql);
        SQLExecDirect(stmt, cmkSql, SQL_NTS);
    }

    /* Create a CEK definition on the server */
    {
        const char cekSqlBefore[] = "CREATE COLUMN ENCRYPTION KEY CustomCEK WITH VALUES ("
            "COLUMN_MASTER_KEY = CustomCMK,"
            "ALGORITHM = 'none',"
            "ENCRYPTED_VALUE = 0x";
        char *cekSql = malloc(sizeof(cekSqlBefore) + 2 * ECEKlen + 2); /* 1 for ')', 1 for null terminator */
        strcpy(cekSql, cekSqlBefore);
        for (i = 0; i < ECEKlen; i++)
            sprintf(cekSql + sizeof(cekSqlBefore) - 1 + 2 * i, "%02x", ECEK[i]);
        strcat(cekSql, ")");
        printf("Create CEK: %s\n", cekSql);
        SQLExecDirect(stmt, cekSql, SQL_NTS);
        free(cekSql);
#ifdef _WIN32
        LocalFree(ECEK);
#else
        free(ECEK);
#endif
    }

#ifdef _WIN32
    FreeLibrary(hProvLib);
#else
    dlclose(hProvLib);
#endif

    /* Create a table with encrypted columns */
    {
        static char *tableSql = "CREATE TABLE CustomKSPTestTable ("
            "c1 int,"
            "c2 varchar(255) COLLATE Latin1_General_BIN2 ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = CustomCEK, ENCRYPTION_TYPE = DETERMINISTIC, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'))";
        printf("Create table: %s\n", tableSql);
        SQLExecDirect(stmt, tableSql, SQL_NTS);
    }

    /* Load provider into the ODBC Driver and configure it */
    {
        unsigned char ksd[sizeof(CEKEYSTOREDATA) + sizeof(PROV_ENCRYPT_KEY) - 1];
        CEKEYSTOREDATA *pKsd = (CEKEYSTOREDATA*)ksd;
        pKsd->name = L"MyCustomKSPName";
        pKsd->dataSize = sizeof(PROV_ENCRYPT_KEY) - 1;
        memcpy(pKsd->data, PROV_ENCRYPT_KEY, sizeof(PROV_ENCRYPT_KEY) - 1);
#ifdef _WIN32
        rc = SQLSetConnectAttr(dbc, SQL_COPT_SS_CEKEYSTOREPROVIDER, "MyKSP.dll", SQL_NTS);
#else
        rc = SQLSetConnectAttr(dbc, SQL_COPT_SS_CEKEYSTOREPROVIDER, "./MyKSP.so", SQL_NTS);
#endif
        checkRC(rc, "Loading KSP into ODBC Driver", 7, dbc, SQL_HANDLE_DBC);
        rc = SQLSetConnectAttr(dbc, SQL_COPT_SS_CEKEYSTOREDATA, (SQLPOINTER)pKsd, SQL_IS_POINTER);
        checkRC(rc, "Configuring the KSP", 7, dbc, SQL_HANDLE_DBC);
    }

    /* Insert some data */
    {
        int c1;
        char c2[256];
        rc = SQLBindParameter(stmt, 1, SQL_PARAM_INPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, &c1, 0, 0);
        checkRC(rc, "Binding parameters for insert", 9, stmt, SQL_HANDLE_STMT);
        rc = SQLBindParameter(stmt, 2, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_VARCHAR, 255, 0, c2, 255, 0);
        checkRC(rc, "Binding parameters for insert", 9, stmt, SQL_HANDLE_STMT);
        for (i = 0; i < 10; i++) {
            c1 = i * 10 + i + 1;
            sprintf(c2, "Sample data %d for column 2", i);
            rc = SQLExecDirect(stmt, "INSERT INTO CustomKSPTestTable (c1, c2) values (?, ?)", SQL_NTS);
            checkRC(rc, "Inserting rows query", 10, stmt, SQL_HANDLE_STMT);
        }
        printf("(Encrypted) data has been inserted into the [CustomKSPTestTable]. You may inspect the data now.\n"
            "Press Enter to continue...\n");
        getchar();
    }

    /* Retrieve the data */
    {
        int c1;
        char c2[256];
        rc = SQLBindCol(stmt, 1, SQL_C_LONG, &c1, 0, 0);
        checkRC(rc, "Binding columns for select", 11, stmt, SQL_HANDLE_STMT);
        rc = SQLBindCol(stmt, 2, SQL_C_CHAR, c2, sizeof(c2), 0);
        checkRC(rc, "Binding columns for select", 11, stmt, SQL_HANDLE_STMT);
        rc = SQLExecDirect(stmt, "SELECT c1, c2 FROM CustomKSPTestTable", SQL_NTS);
        checkRC(rc, "Retrieving rows query", 12, stmt, SQL_HANDLE_STMT);
        while (SQL_SUCCESS == (rc = SQLFetch(stmt)))
            printf("Retrieved data: c1=%d c2=%s\n", c1, c2);
        SQLFreeStmt(stmt, SQL_CLOSE);
        printf("Press Enter to clean up and exit...\n");
        getchar();
    }

    /* Clean up */
    {
        SQLExecDirect(stmt, "DROP TABLE CustomKSPTestTable", SQL_NTS);
        SQLExecDirect(stmt, "DROP COLUMN ENCRYPTION KEY CustomCEK", SQL_NTS);
        SQLExecDirect(stmt, "DROP COLUMN MASTER KEY CustomCMK", SQL_NTS);
        printf("Removed table, CEK, and CMK\n");
    }
    SQLDisconnect(dbc);
    SQLFreeHandle(SQL_HANDLE_DBC, dbc);
    SQLFreeHandle(SQL_HANDLE_ENV, env);
    return 0;
}

```

## See Also

[Using Always Encrypted with the ODBC Driver](using-always-encrypted-with-the-odbc-driver.md)
