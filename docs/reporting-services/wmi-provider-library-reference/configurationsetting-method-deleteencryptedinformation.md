---
title: "DeleteEncryptedInformation method (WMI MSReportServer_ConfigurationSetting)"
description: "DeleteEncryptedInformation method (WMI MSReportServer_ConfigurationSetting)"
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: wmi-provider-library-reference
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "DeleteEncryptedInformation method"
apilocation: "reportingservices.mof"
apiname: "DeleteEncryptedInformation (WMI MSReportServer_ConfigurationSetting Class)"
apitype: MOFDef
---
# ConfigurationSetting method - DeleteEncryptedInformation
  Deletes the encrypted information from the report server database.  
  
## Syntax  
  
```vb  
Public Sub DeleteEncryptedInformation(ByRef HRESULT As Int32, ByRef ExtendedErrors() As String)  
```  
  
```csharp  
public void DeleteEncryptedInformation(out Int32 HRESULT, out string[] ExtendedErrors);  
```  
  
## Parameters  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
 *ExtendedErrors[]*  
 [out] A string array containing other errors returned by the call.  
  
## Return value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A nonzero value indicates that an error occurred.  
  
## Remarks  
 When this method is invoked, the following data is deleted:  
  
-   Data source information that is encrypted, including user name and password.  
  
-   Subscription data that is encrypted using the delivery extension interfaces.  
  
-   All the information from the keys table in the report server database.  
  
 After this method is invoked, you must initialize each computer that uses the report server database.  
  
 Calling the *DeleteEncryptedInformation* method doesn't affect the report server configuration file.  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## Related content

- [MSReportServer_ConfigurationSetting members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)
