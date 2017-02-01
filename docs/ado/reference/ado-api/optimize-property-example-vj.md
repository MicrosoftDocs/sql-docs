---
title: "Optimize Property Example (VJ++) | Microsoft Docs"
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
  - "Optimize property [ADO], VJ++ example"
ms.assetid: a75d5239-54a9-4eec-b144-a5848cdbf265
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Optimize Property Example (VJ++)
This example demonstrates the [Field](../../../ado/reference/ado-api/field-object.md) object dynamic **Optimize** property. The ***zip*** field of the **Authors** table in the ***Pubs*** database is not indexed. Setting the [Optimize](../../../ado/reference/ado-api/optimize-property-dynamic-ado.md) property to **True** on the ***zip*** field authorizes ADO to build an index that improves the performance of the [Find](../../../ado/reference/ado-api/find-method-ado.md) method.  
  
```  
// BeginOptimizeJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class OptimizeX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      OptimizeX();  
      System.exit(0);  
   }  
  
   // OptimizeX function  
  
   static void OptimizeX()  
   {  
  
      // Define ADO Objects.  
      Recordset rstAuthors = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
         + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
      try  
      {  
         rstAuthors = new Recordset();  
         rstAuthors.setCursorLocation(AdoEnums.CursorLocation.CLIENT);  
         // Enable index creation.  
         rstAuthors.open("SELECT * FROM Authors",  
               strCnn,  
               AdoEnums.CursorType.STATIC,  
               AdoEnums.LockType.READONLY,  
               AdoEnums.CommandType.TEXT);  
         rstAuthors.getField("zip").getProperties().  
            getItem("Optimize").setBoolean(true); // Create the index.  
         rstAuthors.find("zip = '94595'");   // Find Akiko Yokomoto.  
         System.out.println(rstAuthors.getField("au_fname").getString() +   
            " " + rstAuthors.getField("au_lname").getString() + " " +   
            rstAuthors.getField("address").getString() + " " +   
            rstAuthors.getField("city").getString() + " " +   
            rstAuthors.getField("state").getString() + " ");  
         rstAuthors.getField("zip").getProperties().  
            getItem("Optimize").setBoolean(false); // Delete the index.  
  
         System.out.println("\nPress <Enter> to continue..");  
         in.readLine();  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstAuthors != null)  
         {  
            PrintProviderError(rstAuthors.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
  
      // System read requires this catch.  
      catch( java.io.IOException je)  
      {  
         PrintIOError(je);  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstAuthors != null)  
            if (rstAuthors.getState() == 1)  
               rstAuthors.close();  
      }  
   }  
  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connections Errors collection.  
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
// EndOptimizeJ  
  
```  
  
## See Also  
 [Field Object](../../../ado/reference/ado-api/field-object.md)   
 [Optimize Property-Dynamic (ADO)](../../../ado/reference/ado-api/optimize-property-dynamic-ado.md)