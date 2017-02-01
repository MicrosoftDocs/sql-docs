---
title: "Append and CreateParameter Methods Example (VJ++) | Microsoft Docs"
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
  - "Append method [ADO], VJ++ example"
  - "CreateParameter method [ADO], VJ++ example"
ms.assetid: 9673f232-fa58-4439-995a-b4066db628aa
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "jhubbard"
---
# Append and CreateParameter Methods Example (VJ++)
This example uses the [Append](../../../ado/reference/ado-api/append-method-ado.md) and [CreateParameter](../../../ado/reference/ado-api/createparameter-method-ado.md) methods to execute a stored procedure with an input parameter.  
  
```  
// BeginAppendJ  
import com.ms.wfc.data.*;  
import java.io.*;  
import com.ms.com.*;  
  
public class AppendX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      AppendX();  
   }  
  
   // AppendX function  
  
   static void AppendX()  
   {  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Command cmdByRoyalty = null;  
      Parameter prmByRoyalty = null;  
      Recordset rstByRoyalty = null;  
      Recordset rstAuthors = null;  
  
      //Declarations.  
      String strCnn;  
      String strAuthorID;  
      String strFName;  
      String strLName;  
      int intRoyalty ;  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader (System.in));  
      String line = null;  
      Variant varRoyalty;  
  
      try  
      {  
         // Open a connection.  
  
         strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
            + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);  
         cnConn1.setCursorLocation(AdoEnums.CursorLocation.CLIENT);  
  
         // Open command object with one parameter.  
         cmdByRoyalty = new Command();  
         cmdByRoyalty.setCommandText("byRoyalty");  
         cmdByRoyalty.setCommandType(AdoEnums.CommandType.STOREDPROC);  
  
         //Get parameter value and append parameter.  
         System.out.println ("\nEnter Royalty : ");  
         line = in.readLine().trim();  
         intRoyalty = Integer.parseInt(line);  
         varRoyalty = new Variant(intRoyalty);  
         prmByRoyalty = cmdByRoyalty.createParameter ("percentage",  
            AdoEnums.DataType.INTEGER,  
            AdoEnums.ParameterDirection.INPUT,4,varRoyalty);  
  
         cmdByRoyalty.getParameters().append(prmByRoyalty);  
         prmByRoyalty.setValue(varRoyalty);  
  
         // Create a recordset by executing the command.  
         cmdByRoyalty.setActiveConnection(cnConn1);  
         rstByRoyalty = cmdByRoyalty.execute();  
  
         // Open the Authors table to get author names for display.  
         rstAuthors = new Recordset ();  
         rstAuthors.open("authors", cnConn1,   
            AdoEnums.CursorType.FORWARDONLY,  
            AdoEnums.LockType.READONLY,   
            AdoEnums.CommandType.TABLE );  
  
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
         System.out.println("\n\nPress <Enter> key to continue.");  
         line = in.readLine();  
  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // Check for null pointer for connection object.  
         if(rstByRoyalty.getActiveConnection()==null)  
            System.out.println("Exception: " + ae.getMessage());  
         // As passing a Recordset, check for null pointer first.  
         if (rstByRoyalty != null)  
         {  
            PrintProviderError(rstByRoyalty.getActiveConnection());  
         }  
         if (rstAuthors != null)  
         {  
            if (rstAuthors.getActiveConnection()==null)  
               System.out.println("Exception: " + ae.getMessage());  
            else  
               PrintProviderError(rstAuthors.getActiveConnection());  
         }  
         else   
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
  
      // This catch is required if input string cannot be converted to  
      // Integer data type.  
      catch( java.lang.NumberFormatException ne)  
      {  
         System.out.println("\nException: Integer Number required.");  
         System.exit(0);  
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
         System.exit(0);//required here only.  
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
  
// EndAppendJ  
```  
  
## See Also  
 [Append Method (ADO)](../../../ado/reference/ado-api/append-method-ado.md)   
 [CreateParameter Method (ADO)](../../../ado/reference/ado-api/createparameter-method-ado.md)