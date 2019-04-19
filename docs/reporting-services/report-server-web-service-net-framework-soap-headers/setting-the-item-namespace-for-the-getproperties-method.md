---
title: "Setting the Item Namespace for the GetProperties Method | Microsoft Docs"
ms.date: 03/06/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: report-server-web-service-net-framework-soap-headers


ms.topic: reference
helpviewer_keywords: 
  - "item properties [Reporting Services]"
  - "ItemNamespaceHeader SOAP header"
  - "GetProperties method"
ms.assetid: b0a08639-3101-40a2-abe2-3a41753826d1
author: maggiesMSFT
ms.author: maggies
---
# Setting the Item Namespace for the GetProperties Method
  You can use the <xref:ReportService2010.ItemNamespaceHeader> SOAP header in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to retrieve item properties based on two different item identifiers: the full path of the item or the ID of the item.  
  
 When you make a call to the <xref:ReportService2010.ReportingService2010.GetProperties%2A> method, you normally pass as an argument the full path of the item for which you want to retrieve properties. By using <xref:ReportService2010.ItemNamespaceHeader>, you can set the SOAP header for your method call to enable you to use <xref:ReportService2010.ReportingService2010.GetProperties%2A> by passing the ID of the item as an identifier.  
  
 The following code sample retrieves the values for item properties based on the ID of the item.  
  
> [!NOTE]  
>  By default, you do not need to set a value for the <xref:ReportService2010.ItemNamespaceHeader> if you pass to the <xref:ReportService2010.ReportingService2010.GetProperties%2A> method the full path name as the item identifier.  
  
```vb  
Imports System  
Imports System.Collections  
  
Class Sample  
   Sub Main()  
      Dim rs As New ReportingService2010()  
      rs.Credentials = System.Net.CredentialCache.DefaultCredentials  
      rs.Url = "https://<Server Name>/reportserver/ReportService2010.asmx"  
  
      Dim items() As CatalogItem  
  
      Try  
         ' Need the ID property of items. Normally, you would already have   
         ' this stored somewhere.  
         items = rs.ListChildren("/AdventureWorks Sample Reports", False)  
  
         ' Set the item namespace header to be GUID-based  
         rs.ItemNamespaceHeaderValue = New ItemNamespaceHeader()  
         rs.ItemNamespaceHeaderValue.ItemNamespace = ItemNamespaceEnum.GUIDBased  
  
         ' Call GetProperties with item ID.  
         If Not (items Is Nothing) Then  
            Dim item As CatalogItem  
            For Each item In  items  
               Dim properties As [Property]() = rs.GetProperties(item.ID, Nothing)  
               Dim property As [Property]  
               For Each property In  properties  
                  Console.WriteLine(([property].Name + ": " + [property].Value))  
               Next property  
               Console.WriteLine()  
            Next item  
         End If  
  
      Catch e As Exception  
         Console.WriteLine((e.Message + ": " + e.StackTrace))  
      End Try  
   End Sub 'Main  
End Class 'Sample  
```  
  
```csharp  
using System;  
using System.Collections;  
  
class Sample  
{  
   static void Main()  
   {  
   ReportingService2010 rs = new ReportingService2010();  
      rs.Credentials = System.Net.CredentialCache.DefaultCredentials;  
      rs.Url = "https://<Server Name>/reportserver/ReportService2010.asmx";  
  
      CatalogItem[] items;  
  
      try  
      {  
         // Need the ID property of items. Normally, you would already have   
         // this stored somewhere.  
         items = rs.ListChildren("/AdventureWorks Sample Reports", false);  
  
         // Set the item namespace header to be GUID-based  
         rs.ItemNamespaceHeaderValue = new ItemNamespaceHeader();  
         rs.ItemNamespaceHeaderValue.ItemNamespace = ItemNamespaceEnum.GUIDBased;  
  
         // Call GetProperties with item ID.  
         if (items != null)  
         {  
            foreach( CatalogItem item in items)  
            {  
               Property[] properties = rs.GetProperties(item.ID, null);  
               foreach (Property property in properties)  
               {  
                  Console.WriteLine(property.Name + ": " + property.Value);  
               }  
               Console.WriteLine();  
            }  
         }  
      }  
  
      catch (Exception e)  
      {  
         Console.WriteLine(e.Message);  
      }  
   }  
}  
```  
  
## See Also  
 [Technical Reference &#40;SSRS&#41;](../../reporting-services/technical-reference-ssrs.md)   
 [Using Reporting Services SOAP Headers](../../reporting-services/report-server-web-service-net-framework-soap-headers/using-reporting-services-soap-headers.md)  
  
  
