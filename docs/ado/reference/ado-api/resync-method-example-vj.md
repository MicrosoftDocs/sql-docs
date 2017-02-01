---
title: "Resync Method Example (VJ++) | Microsoft Docs"
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
  - "Resync method [ADO], VJ++ example"
ms.assetid: 0ef0ed20-83ac-4047-9294-506fd82f7201
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Resync Method Example (VJ++)
This example demonstrates using the [Resync](../../../ado/reference/ado-api/resync-method.md) method to refresh data in a static recordset.  
  
```  
// BeginResyncJ  
import java.io.*;  
import com.ms.wfc.data.*;  
  
public class ResyncX  
{  
   // The main entry point of the application.  
   public static void main (String[] args)  
   {  
      ResyncX();  
      System.exit(0);  
   }  
   static void ResyncX()  
   {  
      // Define ADO Objects.  
      Recordset rstTitles = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';" +  
                  "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
      try  
      {  
         // Open recordset for Titles table.  
         rstTitles = new Recordset();  
         rstTitles.setCursorLocation(AdoEnums.CursorLocation.CLIENT);  
         rstTitles.setCursorType(AdoEnums.CursorType.STATIC);  
         rstTitles.setLockType(AdoEnums.LockType.BATCHOPTIMISTIC);  
         rstTitles.open("Titles", strCnn, AdoEnums.CursorType.STATIC,   
            AdoEnums.LockType.BATCHOPTIMISTIC,   
            AdoEnums.CommandType.TABLE);  
  
         // Change the type of the first title in the recordset.  
         rstTitles.getField("type").setString("database");  
  
         // Display the results of the change.  
         System.out.println("\n\n\tBefore resync:\n" + "\tTitle - " +  
            rstTitles.getField("title").getString() +  
            "\n\tType  - " + rstTitles.getField("type").getString());  
  
         // Resync with database and redisplay the results.  
         rstTitles.resync();  
         System.out.println("\n\n\tAfter resync:\n" + "\tTitle - " +  
            rstTitles.getField("title").getString() +  
            "\n\tType  - " +   
            rstTitles.getField("type").getString()+"\n");  
         rstTitles.cancelBatch();  
  
         System.out.println("\tPress <Enter> to continue..");  
         in.readLine();  
  
      }  
      catch(AdoException ae)  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a recordset, check for null pointer first.  
         if(rstTitles != null)  
         {  
            PrintProviderError(rstTitles.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
      // System read requires this catch.  
      catch(java.io.IOException je)  
      {  
         PrintIOError(je);  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstTitles != null)  
            if (rstTitles.getState() == 1)  
               rstTitles.close();  
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
// EndResyncJ  
  
```  
  
## See Also  
 [Resync Method](../../../ado/reference/ado-api/resync-method.md)