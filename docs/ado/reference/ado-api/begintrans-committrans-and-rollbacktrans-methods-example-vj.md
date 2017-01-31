---
$title: "BeginTrans, CommitTrans, and RollbackTrans Methods Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "BeginTrans method [ADO], VJ++ example"
  - "CommitTrans method [ADO], VJ++ example"
  - "RollbackTrans method [ADO], VJ++ example"
ms.assetid: 27f502f8-d66a-4b44-9071-a05993d3fabb
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# BeginTrans, CommitTrans, and RollbackTrans Methods Example (VJ++)
This example changes the book type of all psychology books in the ***Titles*** table of the database. After the [BeginTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md) method starts a transaction that isolates all the changes made to the ***Titles*** table, the [CommitTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md) method saves the changes. You can use the [Rollback](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md) method to undo changes that you saved using the [Update](../../../ado/reference/ado-api/update-method.md) method.  
  
```  
// BeginBeginTransJ  
import com.ms.wfc.data.*;  
import java.io.*;  
  
public class BeginTransX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      BeginTransX();  
      System.exit(0);  
   }  
  
   // BeginTransX function  
   static void BeginTransX()  
   {  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Recordset rstTitles = null;  
  
      //Declarations.  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
         + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      String strTitle;  
      String strType;  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader (System.in));  
      String line = null;  
      String strMessage="";  
      int intChoice = 0;  
      int recordDisplaySize = 15;  
      int recordCount = 0;  
  
      try  
      {  
         // Open a connection.  
  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn,"","",AdoEnums.CommandType.UNSPECIFIED);  
  
         // Open the Titles table.  
         rstTitles = new Recordset ();  
         rstTitles.setCursorType(AdoEnums.CursorType.DYNAMIC);  
         rstTitles.setLockType(AdoEnums.LockType.PESSIMISTIC);  
         rstTitles.open("titles",cnConn1,AdoEnums.CursorType.DYNAMIC ,  
            AdoEnums.LockType.PESSIMISTIC ,AdoEnums.CommandType.TABLE );  
  
         rstTitles.moveFirst();  
         cnConn1.beginTrans();  
  
         // Loop through recordset and ask user if he/she wants  
         // to change the type for a specified title.  
  
         while (!rstTitles.getEOF())  
         {  
            strType = rstTitles.getField("type").getString().trim();  
            if ((rstTitles.getField("type").getString().  
               trim()).compareTo("psychology")== 0)  
            {  
               strTitle = rstTitles.getField("title").getString().trim();  
               System.out.println("\nTitle : " + strTitle + "\n\n"   
                  +  "Change type to self help (1 -> Yes / 2 -> No) ?");  
               // Change the title for the specified book.  
               line = in.readLine().trim();  
               intChoice = Integer.parseInt(line);  
               if ( intChoice == 1)  
               {  
                  rstTitles.getField("type").setString("self_help");  
                  rstTitles.update();  
               }  
            }  
            rstTitles.moveNext();  
         }  
  
         // Ask if the user wants to commit to all the   
         // changes made above.  
         System.out.println  
            ("\n\nSave all changes (1 -> Yes / 2 -> No) ?");  
         line = in.readLine().trim();  
         intChoice = Integer.parseInt(line);  
         if ( intChoice == 1)  
            cnConn1.commitTrans();  
         else  
            cnConn1.rollbackTrans();  
  
         // Print current data in recordset.  
         rstTitles.requery();  
         rstTitles.moveFirst();  
  
         while(true)  
         {  
            strMessage = strMessage +"\n " +   
               rstTitles.getField("title").getString()+" - "  
               + rstTitles.getField("type").getString() ;  
  
            if ( recordCount == recordDisplaySize)  
            {  
               System.out.println(strMessage);  
               System.out.println("\n\nPress <Enter> key to continue..");  
               line = in.readLine();  
               strMessage = "";  
               recordCount = 0;  
            }  
            recordCount++;  
            rstTitles.moveNext();  
            if(rstTitles.getEOF())  
            {  
               System.out.println(strMessage);  
               break;  
            }  
         }  
         System.out.println("\n\nPress <Enter> key to continue..");  
         line = in.readLine();  
  
         // Restore original data because this   
         // is a demonstration.  
         rstTitles.moveFirst();  
         while (!rstTitles.getEOF())  
         {  
            if ((rstTitles.getField("type").getString().  
               trim().compareTo("self_help"))== 0)  
            {  
               rstTitles.getField("type").setString("psychology");  
               rstTitles.update();  
            }  
            rstTitles.moveNext();  
         }  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // Check for null pointer for connection object.  
         if(rstTitles.getActiveConnection()==null)  
            System.out.println("Exception: " + ae.getMessage());  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstTitles != null)  
         {  
            PrintProviderError(rstTitles.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
      // System Read requires this catch.  
      catch( java.io.IOException je )  
      {  
         PrintIOError(je);  
      }        
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstTitles != null)  
            if (rstTitles.getState() == 1)  
               rstTitles.close();    
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
  
   //.PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
  
// EndBeginTransJ  
```  
  
## See Also  
 [BeginTrans, CommitTrans, and RollbackTrans Methods (ADO)](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md)   
 [Update Method](../../../ado/reference/ado-api/update-method.md)