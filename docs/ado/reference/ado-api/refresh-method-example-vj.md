---
title: "Refresh Method Example (VJ++) | Microsoft Docs"
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
  - "Refresh method [ADO], VJ++ example"
ms.assetid: c0fbf728-0ccb-468d-be1e-c09dad9ffddb
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Refresh Method Example (VJ++)
This example demonstrates using the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method to refresh the [Parameters](../../../ado/reference/ado-api/parameters-collection-ado.md) collection for a stored procedure [Command](../../../ado/reference/ado-api/command-object-ado.md) object.  
  
```  
// BeginRefreshJ  
import  com.ms.wfc.data.*;  
import java.io.*;  
  
public class RefreshX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      RefreshX();  
      System.exit(0);  
   }  
  
   // RefreshX function  
  
   static void RefreshX()  
   {  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Command cmdByRoyalty = null;  
      Recordset rstByRoyalty = null;  
      Recordset rstAuthors = null;  
  
      //Declarations.  
      String strAuthorID;  
      String strFName;  
      String strLName;  
      int intRoyalty ;  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader (System.in));  
      String line = null;  
  
      try  
      {  
         // Open a connection.  
         String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
                     + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);  
  
         // Open a command object for a stored procedure  
         // with one parameter.  
         cmdByRoyalty = new Command();  
         cmdByRoyalty.setActiveConnection(cnConn1);  
         cmdByRoyalty.setCommandText("byRoyalty");  
         cmdByRoyalty.setCommandType(AdoEnums.CommandType.STOREDPROC);  
         cmdByRoyalty.getParameters().refresh();  
  
         // Get Parameter value and execute the command  
         // storing the results in the recordset.  
         System.out.println ("\nEnter Royalty : ");  
         line = in.readLine().trim();  
         intRoyalty = Integer.parseInt(line);  
         cmdByRoyalty.getParameters().getItem(1).setInt(intRoyalty);  
  
         // Create a recordset by executing the command.  
         rstByRoyalty = cmdByRoyalty.execute();  
  
         // Open the Authors table to get author names for display.  
         rstAuthors = new Recordset ();  
         rstAuthors.open(  
            "Authors",strCnn,AdoEnums.CursorType.FORWARDONLY,   
            AdoEnums.LockType.READONLY, AdoEnums.CommandType.TABLE );  
  
         // Print current data in the recordset,  
         // adding author names from Authors table.  
         System.out.println("\nAuthors with " + intRoyalty +  
                        " percent royalty");  
         while (!rstByRoyalty.getEOF())  
         {  
            strAuthorID =  rstByRoyalty.getField("au_id").getString();  
            rstAuthors.setFilter("au_id ='" +  strAuthorID + "'");  
            strFName = rstAuthors.getField("au_fname").getString();  
            strLName = rstAuthors.getField("au_lname").getString();  
            System.out.println("\t" + strAuthorID + ", " + strFName  
                           + " " + strLName);  
            rstByRoyalty.moveNext();  
         }  
         System.out.println("\n\nPress <Enter> key to continue..");  
         line = in.readLine();  
  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstByRoyalty != null)  
         {  
            PrintProviderError(rstByRoyalty.getActiveConnection());  
         }  
         else if (rstAuthors != null)  
         {  
            PrintProviderError(rstAuthors.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
  
      // This catch is required if input string cannot be converted to  
      // Integer data type.  
      catch ( java.lang.NumberFormatException ne)  
      {  
            System.out.println("\nException: Integer Input required.");  
      }  
      // System Read requires this catch.  
      catch( java.io.IOException je )  
      {  
         PrintIOError(je);  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstByRoyalty != null)  
            if (rstByRoyalty.getState() == 1)  
               rstByRoyalty.close();    
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
// EndRefreshJ  
  
```  
  
## See Also  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)   
 [Parameters Collection (ADO)](../../../ado/reference/ado-api/parameters-collection-ado.md)   
 [Refresh Method (ADO)](../../../ado/reference/ado-api/refresh-method-ado.md)