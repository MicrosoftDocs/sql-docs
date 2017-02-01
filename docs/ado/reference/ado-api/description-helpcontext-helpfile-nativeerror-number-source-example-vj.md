---
title: "Error Object Properties Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "HelpFile property [ADO], VJ++ example"
  - "HelpContext property [ADO], VJ++ example"
  - "Source property [ADO], VJ++ example"
  - "NativeError property [ADO], VJ++ example"
  - "SQLState property [ADO], VJ++ example"
  - "Number property [ADO], VJ++ example"
  - "Description property [ADO], VJ++ example"
ms.assetid: 7fd0eebc-99f4-490e-9b62-0b62b1884d6b
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "jhubbard"
---
# Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VJ++)
This example triggers an error, traps it, and displays the [Description](../../../ado/reference/ado-api/description-property.md), [HelpContext](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md), [HelpFile](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md), [NativeError](../../../ado/reference/ado-api/nativeerror-property-ado.md), [Number](../../../ado/reference/ado-api/number-property-ado.md), [Source](../../../ado/reference/ado-api/source-property-ado-error.md), and [SQLState](../../../ado/reference/ado-api/sqlstate-property.md) properties of the resulting [Error](../../../ado/reference/ado-api/error-object.md) object.  
  
```  
// BeginDescriptionJ  
// The WFC class includes the ADO objects.  
  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class DescriptionX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      DescriptionX();  
      System.exit(0);  
   }  
  
   // DescriptionX function  
  
   static void DescriptionX()  
   {  
      // Declarations.  
      BufferedReader in = new   
         BufferedReader(new InputStreamReader(System.in));  
  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
  
      try  
      {  
         // Intentionally trigger an error.  
         cnConn1 = new Connection();  
         cnConn1.open("nothing");  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
         PrintProviderError(cnConn1);  
      }  
  
      try  
      {  
         System.out.println("\nPress <Enter> key to continue.");  
         in.readLine();  
      }  
      // System read requires this catch.  
      catch( java.io.IOException je)  
      {  
         PrintIOError(je);  
      }  
  
   }  
  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connection's Errors collection.  
      com.ms.wfc.data.Error  ErrItem = null;  
      long nCount = 0;  
      int  i      = 0;  
  
      nCount = Cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if( nCount > 0);  
      {  
         // Collection ranges from 0 to nCount - 1  
         for (i = 0; i< nCount; i++)  
         {  
            ErrItem = Cnn1.getErrors().getItem(i);  
            System.out.println("\t Error number: " + ErrItem.getNumber()  
               + "\t" + ErrItem.getDescription() );  
         }  
      }  
  
   }  
  
   // PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
// EndDescriptionJ  
```  
  
## See Also  
 [Description Property](../../../ado/reference/ado-api/description-property.md)   
 [Error Object](../../../ado/reference/ado-api/error-object.md)   
 [HelpContext, HelpFile Properties](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md)   
 [HelpContext, HelpFile Properties](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md)   
 [NativeError Property (ADO)](../../../ado/reference/ado-api/nativeerror-property-ado.md)   
 [Number Property (ADO)](../../../ado/reference/ado-api/number-property-ado.md)   
 [Source Property (ADO Error)](../../../ado/reference/ado-api/source-property-ado-error.md)   
 [SQLState Property](../../../ado/reference/ado-api/sqlstate-property.md)
