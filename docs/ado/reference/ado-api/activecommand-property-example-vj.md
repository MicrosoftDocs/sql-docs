---
title: "ActiveCommand Property Example (VJ++) | Microsoft Docs"
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
  - "ActiveCommand property [ADO], VJ++ example"
ms.assetid: f28637c7-05ab-482d-b1ce-bbfc41228050
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# ActiveCommand Property Example (VJ++)
This example demonstrates the [ActiveCommand](../../../ado/reference/ado-api/activecommand-property-ado.md) property.  
  
 A subroutine is given a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object whose **ActiveCommand** property is used to display the command text and parameter that created the **Recordset**.  
  
```  
// BeginActiveCommandJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class ActiveCommandX     
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      ActiveCommandX();  
      System.exit(0);  
   }  
  
   // ActiveCommandX function  
  
   static void ActiveCommandX()  
   {  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Command cmd = null;  
      Recordset rstAuthors = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
         + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      String strName;  
  
      try  
      {  
         System.out.println("Enter an author's name (e.g., Ringer): ");  
         strName = in.readLine().trim();  
         cmd = new Command();  
         cmd.setCommandText("SELECT * FROM authors WHERE au_lname = ?");  
         cmd.getParameters().append(cmd.createParameter("LastName",  
            AdoEnums.DataType.CHAR,  
            AdoEnums.ParameterDirection.INPUT, 20, strName));  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);  
         cmd.setActiveConnection(cnConn1);  
         rstAuthors = cmd.execute(null,AdoEnums.CommandType.TEXT);  
         ActiveCommandXprint(rstAuthors);  
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
         // Cleanup objects before exit.     
         if (cnConn1 != null)  
            if (cnConn1.getState() == 1)  
               cnConn1.close();  
      }  
   }  
  
   // ActiveCommandXprint function  
   static void ActiveCommandXprint(Recordset rstp)  
   {  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      String strName;  
  
      try  
      {  
         strName = rstp.getActiveCommand().getParameters().  
            getItem("LastName").getValue().toString();  
         System.out.println("\nCommand text = '" +  
            rstp.getActiveCommand().getCommandText() + "'");  
         System.out.println("Parameter = '" + strName + "'");  
         if(rstp.getBOF())  
         {  
            System.out.println("Name = '" + strName + "', not found.");  
         }  
         else  
         {  
            System.out.println("Name = '" +  
               rstp.getField("au_fname").getString() + " " +   
               rstp.getField("au_lname").getString() +   
               "', author ID = '" +   
               rstp.getField("au_id").getString() + "'");  
         }  
         System.out.println("\nPress <Enter> to continue..");  
         in.readLine();  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstp != null)  
         {  
            PrintProviderError(rstp.getActiveConnection());  
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
  
// EndActiveCommandJ  
```  
  
## See Also  
 [ActiveCommand Property (ADO)](../../../ado/reference/ado-api/activecommand-property-ado.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)