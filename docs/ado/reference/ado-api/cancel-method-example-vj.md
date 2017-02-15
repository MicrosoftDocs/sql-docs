---
title: "Cancel Method Example (VJ++) | Microsoft Docs"
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
  - "Cancel method [ADO], VJ++ example"
ms.assetid: 3e41ee6f-5138-4d32-98ac-05e30a2a6fd2
caps.latest.revision: 10
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Cancel Method Example (VJ++)
This example uses the [Cancel](../../../ado/reference/ado-api/cancel-method-ado.md) method to cancel a command executing on a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object if the connection is busy.  
  
```  
// BeginCancelJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class CancelX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      CancelX();  
      System.exit(0);  
   }  
  
   // CancelX function  
  
   static void CancelX()  
   {  
      // Define command strings.  
      String strCmdChange = "UPDATE titles SET type = 'self_help' "  
         + "WHERE type = 'psychology'";  
      String strCmdRestore = "UPDATE titles SET type = 'psychology' "  
         + "WHERE type = 'self_help'";  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
         + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
  
      //Declarations.  
      boolean booChanged = false;  
      BufferedReader in =  
         new BufferedReader (new InputStreamReader(System.in));  
      String line = null;  
  
      try  
      {  
         // Open a connection.  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);  
  
         // Begin a transaction, then execute a command asynchronously.  
         cnConn1.beginTrans();  
         cnConn1.execute(strCmdChange,  
            AdoEnums.ExecuteOption.ASYNCEXECUTE);  
  
         // do something else for a little while - this could be changed.  
  
         for (int intLoop = 0; intLoop < 10; intLoop++)  
         {  
            System.out.println(intLoop);  
         }  
  
         // If the command has NOT completed, cancel the execute  
         // and roll back the transaction. Otherwise, commit the  
         // transaction.  
  
         if ((cnConn1.getState() & AdoEnums.ObjectState.EXECUTING) > 0)  
         {  
            cnConn1.cancel();  
            cnConn1.rollbackTrans();  
            booChanged = false;  
            System.out.println("\nUpdate canceled.");  
         }  
         else  
         {  
            cnConn1.commitTrans();  
            booChanged = true;  
            System.out.println("\nUpdate complete.");  
         }  
  
         //If the change was made, restore the data  
         // because this is a demonstration.  
  
         if(booChanged )  
         {  
            cnConn1.execute(strCmdRestore);  
            System.out.println("\nData restored.");  
         }  
  
         System.out.println("\n\nPress <Enter> to continue..");  
         in.readLine();  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a connection, check for null pointer first.  
         if (cnConn1 != null)  
         {  
            PrintProviderError(cnConn1);  
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
         if (cnConn1 != null)  
            if (cnConn1.getState() == 1)  
               cnConn1.close();  
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
  
// EndCancelJ  
```  
  
## See Also  
 [Cancel Method (ADO)](../../../ado/reference/ado-api/cancel-method-ado.md)   
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)