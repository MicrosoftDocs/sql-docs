---
title: "ListInstalledSharePointVersions Method (WMI) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: wmi-provider-library-reference


ms.topic: conceptual
helpviewer_keywords: 
  - "ListInstalledSharePointVersions method"
ms.assetid: 8f0a5e9f-23f1-41e5-9a90-dfec19ef1df7
author: markingmyname
ms.author: maghan
---
# ConfigurationSetting Method - ListInstalledSharePointVersions
  Returns a set of tokens that represent the versions of Microsoft [!INCLUDE[winSPServ](../../includes/winspserv-md.md)], [!INCLUDE[offSPServ](../../includes/offspserv-md.md)], [!INCLUDE[SPF2010](../../includes/spf2010-md.md)], or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] that are installed on the same computer as the report server.  
  
## Syntax  
  
```vb  
Public Sub ListInstalledSharePointVersions(ByRef VersionTokens() _  
    As String, ByRef Length As Int32, ByRef HRESULT As Int32)  
```  
  
```csharp  
public void ListReportServersInDatabase (out string[] VersionTokens,   
    out Int32 Length, out Int32 HRESULT);  
```  
  
## Parameters  
 *VersionTokens[]*  
 [out] An array that contains the tokens that represent the version of a SharePoint product or technology that is compatible with the installed report server.  
  
 *Length*  
 [out] The length of the version tokens array.  
  
 *HRESULT*  
 [out] Value indicating whether the call succeeded or failed.  
  
## Return Value  
 Returns an *HRESULT* indicating success or failure of the method call. A value of 0 indicates that the method call was successful. A non-zero value indicates that an error has occurred.  
  
## Remarks  
 Each token that is returned represents a version of [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] or [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] that is compatible with the currently installed report server. If a particular version of SharePoint is compatible with previous SharePoint versions, tokens for each compatible SharePoint version are returned.  
  
 The following is a table of the SharePoint tokens that are returned.  
  
|**Version Tokens**|**Description**|  
|------------------------|---------------------|  
|WSS_V2_Compatible|A [!INCLUDE[winSPServ](../../includes/winspserv-md.md)], [!INCLUDE[winSPServ](../../includes/winspserv-md.md)], [!INCLUDE[offSPServ](../../includes/offspserv-md.md)], [!INCLUDE[SPF2010](../../includes/spf2010-md.md)], or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] installation is installed that is compatible with [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] 2.0.|  
|WSS_V3_Compatible|A [!INCLUDE[winSPServ](../../includes/winspserv-md.md)], [!INCLUDE[winSPServ](../../includes/winspserv-md.md)], [!INCLUDE[offSPServ](../../includes/offspserv-md.md)], [!INCLUDE[SPF2010](../../includes/spf2010-md.md)], or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] installation is installed that is compatible with [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] 3.0.|  
|WSS_V4_Compatible|A [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] installation is installed that is compatible with Office 14.|  
  
## Requirements  
 **Namespace:** [!INCLUDE[ssRSWMInmspcA](../../includes/ssrswminmspca-md.md)]  
  
## See Also  
 [MSReportServer_ConfigurationSetting Members](../../reporting-services/wmi-provider-library-reference/msreportserver-configurationsetting-members.md)  
  
  
