---
title: "ADO Security Design Issues"
description: "ADO Security Design Features"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADO, security"
---
# ADO Security Design Features
The following sections describe security design features in ActiveX Data Objects (ADO) 2.8 and later. These changes were made in ADO 2.8 to improve security. ADO 6.0, which is included in Windows DAC 6.0 in Windows Vista, is functionally equivalent to ADO 2.8, which was included in MDAC 2.8 in Windows XP and Windows Server 2003. This topic provides information about how to best secure your applications in ADO 2.8 or later.

> [!IMPORTANT]
>  If you are updating your application from an earlier version of ADO, it is recommended that you test your updated application on a non-production computer before you deploy it to customers. This way, you can ensure you are aware of any compatibility issues before you deploy your updated application.

## Internet Explorer File Access Scenarios
 The following features effect how ADO 2.8 and later works when it is used in scripted Web pages in Internet Explorer.

### Revised and improved security warning message box now used to alert users
 For ADO 2.7 and earlier, the following warning message appears when a scripted Web page tries to run ADO code from an untrusted provider:

```console
This page accesses data on another domain. Do you want to allow this? To
avoid this message in Internet Explorer, you can add a secure Web site to
your Trusted Sites zone on the Security tab of the Internet Options dialog
box.
```

 For ADO 2.8 and later, the preceding message no longer appears. Instead, the following message appears in this context:

```console
This Website uses a data provider that may be unsafe. If you trust the
Website, click OK, otherwise click Cancel.
```

 The preceding message allows the user to make informed decision, while knowing the consequences for either choice:

-   If user trusts the site, clicking OK will allow all disk-safe code (all ADO methods and properties with the exceptions of the disk-accessible APIs described later in this topic) to run and execute in the browser window.

-   If user does not trust the site, clicking Cancel blocks the ADO code for data access from running and executing in its entirety.

### Disk-accessible code limited now to trusted sites
 Additional design changes were made in ADO 2.8 that specifically restrict the ability of a limited set of APIs, which might expose the potential to read from or write to files on the local computer. Here are the API methods that have been further restricted for safety when running Internet Explorer:

-   For the ADO **Stream** object, if the [LoadFromFile](../reference/ado-api/loadfromfile-method-ado.md) or [SaveToFile](../reference/ado-api/savetofile-method.md) methods are used.

-   For the ADO **Recordset** object, if either the [Save](../reference/ado-api/save-method.md) method or the [Open](../reference/ado-api/open-method-ado-recordset.md) method, such as when either the **adCmdFile** option is set or the [Microsoft OLE DB Persistence Provider (MSPersist)](./appendixes/microsoft-ole-db-persistence-provider-ado-service-provider.md) is used.

 For these limited sets of potentially disk-accessible functions, the following behavior occurs for ADO 2.8 and later, if any code that uses these methods is run in Internet Explorer:

-   If the site that provided the code was added previously to the Trusted Sites zone list, the code executes in the browser and access is granted to local files.

-   If the site does not appear in the Trusted Sites zone list, the code is blocked and access to local files is denied.

    > [!NOTE]
    >  In ADO 2.8 and later, the user is not alerted or advised to add sites to the Trusted Sites zone list. Therefore the management of the Trusted Sites list is the responsibility of those who are deploying or supporting Web site-based applications that require access to the local file system.

### Access blocked to the ActiveCommand property on Recordset objects
 When running in Internet Explorer, ADO 2.8 now blocks access to the [ActiveCommand](../reference/ado-api/activecommand-property-ado.md) property for an active **Recordset** object and returns an error. The error occurs regardless of whether the page comes from a Web site registered in the Trusted Sites list.

### Changes in handling for OLE DB providers and integrated security
 While reviewing ADO 2.7 and earlier versions for potential security issues and concerns, the following scenario was discovered:

 In some instances, OLE DB providers who support the Integrated Security [DBPROP_AUTH_INTEGRATED](/previous-versions/windows/desktop/ms712973(v=vs.85)) property could potentially permit scripted Web pages to reuse the ADO Connection object to connect unintentionally to other servers using the current login credentials of the users. To prevent this, ADO 2.8 and later handle OLE DB providers depending on how they have chosen to provide, or not provide, for integrated security.

 For Web pages that are loaded from sites listed in the Trusted Sites zone list, the following table provides a breakdown of how ADO 2.8 and later manages ADO connections in each case.

|IE settings for user authentication, logon|Provider supports "Integrated Security" and UID and PWD are specified (SQLOLEDB)|Provider does not support "Integrated Security" (JOLT, MSDASQL, MSPersist)|Provider supports "Integrated Security" and it is set to SSPI (no UID/PWD are specified)|
|------------------------------------------------|----------------------------------------------------------------------------------------|----------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------|
|Automatic logon with current username and password|Allow connection|Allow connection|Allow connection|
|Prompt for user name and password|Allow connection|Fail connection|Fail connection|
|Automatic logon only in Intranet zone|Allow connection|Prompt user with security warning|Prompt user with security warning|
|Anonymous logon|Allow connection|Fail connection|Fail connection|

 In the case where a security warning now appears, the message box informs users:

```console
This Website is using your identity to access a data source. If you trust this Website, click OK, otherwise click Cancel.
```

 The preceding message allows the user to make a more informed decision and proceed accordingly.

> [!NOTE]
>  For untrusted sites (that is, sites not listed in the Trusted Sites zone list), if the provider is also untrusted (as discussed earlier in this section), the user might see two security warnings in a row, a warning about the unsafe provider and a second warning about the attempt to use their identity. If the user clicks OK to the first warning, the Internet Explorer settings and response behavior code described in the preceding table are executed.

## Controlling whether password text is returned in ADO connection strings
 When you try get the value of the [ConnectionString](../reference/ado-api/connectionstring-property-ado.md) property on an ADO **Connection** object, the following events occur:

1.  If the connection is open, an initialization call is then made to the underlying OLE DB provider to get the connection string.

2.  Depending on the setting in the OLE DB provider of the [DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO](/previous-versions/windows/desktop/ms714905(v=vs.85)) property, passwords are included together with other connection string information that is returned.

 For example, if the ADO Connection dynamic property **Persist Security Info** is set to **True**, password information is included in the connection string returned. Otherwise, if the underlying provider has set the property to **False** (for example with the SQLOLEDB provider), password information is omitted in the returned connection string.

 If you are using third-party (that is, non-Microsoft) OLE DB providers with your ADO application code, you might check how the **DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO** property is implemented to determine whether inclusion of password information with ADO connection strings is permitted.

## Checking for non-file devices when loading and saving recordsets or streams
 For ADO 2.7 and earlier, file input/output operations such as [Open](../reference/ado-api/open-method-ado-recordset.md) and [Save](../reference/ado-api/save-method.md) that were used to read and write file-based data could in some cases allow a URL or file name to be used that specified a non-disk based file type. For example, LPT1, COM2, PRN.TXT, AUX could be used as an alias for input/output between printers and auxiliary devices on the system using certain

 For ADO 2.8 and later, this functionality has been updated. For opening and saving **Recordset** and **Stream** objects, ADO now performs a file type check to ensure that the input or output device specified in a URL or file name is an actual file.

> [!NOTE]
>  File type checking as described in this section only applies for Windows 2000 and later. It does not apply to situations where ADO 2.8 or later is running under earlier Windows releases, such as Windows 98.