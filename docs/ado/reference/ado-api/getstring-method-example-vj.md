---
$title: "GetString Method Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "GetString method [ADO], VJ++ example"
ms.assetid: d8260e68-e255-4c0c-9f13-5cca6a9a9c35
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# GetString Method Example (VJ++)
This example demonstrates the [GetString](../../../ado/reference/ado-api/getstring-method-ado.md) method.  
  
 Assume you are debugging a data access problem and want a quick, simple way of printing the current contents of a small [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
```  
// BeginGetStringJ  
// The WFC class includes the ADO objects.  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class GetStringX  
{  
    // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      GetStringX ();  
      System.exit(0);  
   }  
  
   // GetStringX  function  
   static void GetStringX()  
   {  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Recordset rstAuthors = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';" +  
            "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      String strOutput;  
  
      try  
      {  
         // Get the user input for state.  
         System.out.println(  
            "Enter a state (CA, IN, KS, MD, MI, OR, TN, UT): ");  
         String strState = in.readLine().trim();  
         String strQuery =   
            "SELECT au_fname, au_lname, address, city FROM Authors " +  
                    "WHERE state = '" + strState + "'";  
  
         // Open recordset with data from Authors table.  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);  
         rstAuthors = new Recordset();  
         rstAuthors.open(strQuery,  
                cnConn1,  
                AdoEnums.CursorType.STATIC,  
                AdoEnums.LockType.READONLY,  
                AdoEnums.CommandType.TEXT);  
  
         if (rstAuthors.getRecordCount() > 0)  
         {  
            // Use all defaults: get all rows, TAB column delimiter,   
            // CARRIAGE RETURN  
            // row delimiter, empty-string null delimiter.  
            strOutput =   
               rstAuthors.getString(AdoEnums.StringFormat.CLIPSTRING,  
               rstAuthors.getRecordCount(),  
               "\t ",  
               "\n",  
               "").trim();  
            System.out.println("\nState = '" + strState + "'" +  
               "\n\n" +  
               "Name             Address             City" +  
               "\n");  
            System.out.println(strOutput);  
         }  
         else  
            System.out.println("\nNo rows found for state = '" +  
               strState + "'\n");  
  
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
// EndGetStringJ  
```  
  
## See Also  
 [GetString Method (ADO)](../../../ado/reference/ado-api/getstring-method-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)