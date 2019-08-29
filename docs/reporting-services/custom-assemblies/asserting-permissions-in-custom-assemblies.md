---
title: "Asserting Permissions in Custom Assemblies | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: custom-assemblies


ms.topic: reference
helpviewer_keywords: 
  - "secure calls [Reporting Services]"
  - "custom assemblies [Reporting Services], permissions"
  - "permission sets [Reporting Services]"
  - "asserting permissions"
  - "permissions [Reporting Services], custom assemblies"
  - "limited permission sets"
  - "security configuration files [Reporting Services]"
ms.assetid: 3afb9631-f15e-405e-990b-ee102828f298
author: maggiesMSFT
ms.author: maggies
---
# Asserting Permissions in Custom Assemblies
  By default, custom assembly code runs with the limited **Execution** permission set. In some cases, you may wish to implement a custom assembly that makes secured calls to protected resources within your security system (such as a file or the registry). In order to accomplish this, you must do the following:  
  
1.  Identify the exact permissions that your code needs in order to make the secured call. If this method is part of a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] library, this information should be included in the method documentation.  
  
2.  Modify the report server policy configuration files in order to grant the custom assembly the required permissions. For more information about the security policy configuration files, see [Using Reporting Services Security Policy Files](../../reporting-services/extensions/secure-development/using-reporting-services-security-policy-files.md).  
  
3.  Assert the required permissions as part of the method in which the secure call is made. This is required because the custom assembly code that is called by the report server is part of the report expression host assembly, which runs with **Execution** permission by default. The **Execution** permission set enables code to run, but not to use protected resources.  
  
4.  Mark the custom assembly with **AllowPartiallyTrustedCallersAttribute** if it is signed with a strong name. This is required because custom assemblies are called from a report expression that is part of the report expression host assembly, which, by default, is not granted **FullTrust**; thus it is a "partially trusted" caller. For more information, see [Using Strong-Named Custom Assemblies](../../reporting-services/custom-assemblies/using-strong-named-custom-assemblies.md).  
  
## Implementing a Secure Call  
 You can modify the policy configuration files to grant your assembly specific permissions. For example, if you were writing a custom assembly to handle currency conversion, you might need to read the current currency exchange rates from a file. To retrieve the rate information, you would need to add an additional security permission, **FileIOPermission**, to your permission set for the assembly. You can make the following additional entry in the policy configuration file:  
  
```  
<PermissionSet class="NamedPermissionSet"  
   version="1"  
   Name="CurrencyRatesFilePermissionSet"  
   Description="A special permission set that grants read access to my currency rates file.">  
      <IPermission class="FileIOPermission"  
         version="1"  
         Read="C:\CurrencyRates.xml"/>  
      <IPermission class="SecurityPermission"  
         version="1"  
         Flags="Execution, Assertion"/>  
</PermissionSet>  
```  
  
 You then add a code group that references that permission set:  
  
```  
<CodeGroup class="UnionCodeGroup"  
   version="1"  
   PermissionSetName="CurrencyRatesFilePermissionSet"  
   Name="MyNewCodeGroup"  
   Description="A special code group for my custom assembly.">  
   <IMembershipCondition class="UrlMembershipCondition"  
      version="1"  
      Url="C:\Program Files\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\MSSQL\Reporting Services\ReportServer\bin\CurrencyConversion.dll"/>  
</CodeGroup>  
```  
  
 In order for your code to acquire the appropriate permission, you must assert the permission within your custom assembly code. For example, if you want to add read-only access to an XML file, C:\CurrencyRates.xml, you must add the following code to your method:  
  
```  
// C#  
FileIOPermission permission = new FileIOPermission(FileIOPermissionAccess.Read, @"C:\CurrencyRates.xml");  
try  
{  
   permission.Assert();  
   // Load the XML currency rates file  
   XmlDocument doc = new XmlDocument();  
   doc.Load(@"C:\CurrencyRates.xml");  
...  
```  
  
 You can also add the assertion as a method attribute:  
  
```  
[FileIOPermissionAttribute(SecurityAction.Assert, Read=@"C:\CurrencyRates.xml")]  
```  
  
 For more information, see ".NET Framework Security" in the .NET Framework Developer's Guide.  
  
## See Also  
 [Using Custom Assemblies with Reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md)  
  
  
