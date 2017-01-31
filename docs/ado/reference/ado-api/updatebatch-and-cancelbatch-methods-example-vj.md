---
$title: "UpdateBatch and CancelBatch Methods Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "UpdateBatch method [ADO]"
  - "CancelBatch method [ADO]"
ms.assetid: 8e8728aa-267f-4468-9a04-8bb29457995c
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# UpdateBatch and CancelBatch Methods Example (VJ++)
This example demonstrates the [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) method in conjunction with the [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) method.  
  
```  
// BeginUpdateBatchJ  
import java.io.*;  
import com.ms.wfc.data.*;  
  
public class UpdateBatchX  
{  
   // The main entry point of the application.  
   public static void main (String[] args)  
   {  
      UpdateBatchX();  
      System.exit(0);  
   }  
   // UpdateBatchX Function  
  
   static void UpdateBatchX()  
   {  
      // Define ADO Objects.  
      Recordset rstTitles = null;  
  
      // Declarations.  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"+  
                  "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      String strTitle;  
      String strMessage;  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
  
      try  
      {  
         rstTitles = new Recordset();  
         rstTitles.setCursorType(AdoEnums.CursorType.KEYSET);  
         rstTitles.setLockType(AdoEnums.LockType.BATCHOPTIMISTIC);  
         rstTitles.open("Titles", strCnn, AdoEnums.CursorType.KEYSET,   
            AdoEnums.LockType.BATCHOPTIMISTIC,   
            AdoEnums.CommandType.TABLE);  
  
         rstTitles.moveFirst();  
  
         // Loop through recordset and ask user if she wants  
         // to change the type for the specified table.  
         while(!rstTitles.getEOF())  
         {  
            if(rstTitles.getField("Type").getString().  
               trim().equalsIgnoreCase("psychology"))  
            {  
               strTitle = rstTitles.getField("Title").getString();  
               strMessage = "Title: "+ strTitle + "\n" +   
                  "Change type to self_help?Enter (Y/N)";  
               System.out.println(strMessage);  
               if(in.readLine().trim().equalsIgnoreCase("Y"))  
                  rstTitles.getField("type").setString("self_help");  
            }  
            rstTitles.moveNext();  
         }  
         // Ask the user if she wants to commit to all the  
         // changes made above.  
         System.out.println("Save all changes?Enter (Y/N)");  
         if(in.readLine().trim().equalsIgnoreCase("Y"))  
            rstTitles.updateBatch();  
         else  
            rstTitles.cancelBatch();  
  
         // Print current data in recordset.  
         rstTitles.requery();  
         rstTitles.moveFirst();  
         while(!rstTitles.getEOF())  
         {  
         System.out.println(rstTitles.getField("title").getString() +  
            " - " + rstTitles.getField("type").getString());  
         rstTitles.moveNext();  
         }  
  
         // Restore original values because this is a demonstration.  
         rstTitles.moveFirst();  
         while(!rstTitles.getEOF())  
         {  
            if(rstTitles.getField("type").getString().  
               trim().equalsIgnoreCase("self_help"))  
               rstTitles.getField("type").setString("psychology");  
            rstTitles.moveNext();  
         }  
         rstTitles.updateBatch();  
  
         System.out.println("Press <Enter> to continue..");  
         in.readLine();  
  
      }  
      catch(AdoException ae)  
      {  
         // Notify the user of any errors that result from ADO.  
  
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
   static void PrintProviderError(Connection cnn1)  
   {  
      // Print Provider Errors from Connection Object.  
      // ErrItem is an item object in the Connections Errors Collection.  
      com.ms.wfc.data.Error               ErrItem = null;  
      long                                 nCount = 0;  
      int                                       i = 0;  
  
      nCount = cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if ( nCount > 0)  
      {  
         // Collection ranges from 0 to nCount-1.  
         for ( i=0;i<nCount; i++)  
         {  
            ErrItem = cnn1.getErrors().getItem(i);  
            System.out.println("\t Error Number: " + ErrItem.getNumber()   
               + "\t" + ErrItem.getDescription());  
         }  
      }  
   }  
   // PrintIOError Function  
   static void PrintIOError(java.io.IOException je)  
   {  
      System.out.println("Error: \n");  
      System.out.println("\t Source: " + je.getClass() + "\n");  
      System.out.println("\t Description: "+ je.getMessage() + "\n");  
   }  
}  
// EndUpdateBatchJ  
  
```  
  
## See Also  
 [CancelBatch Method (ADO)](../../../ado/reference/ado-api/cancelbatch-method-ado.md)   
 [UpdateBatch Method](../../../ado/reference/ado-api/updatebatch-method.md)