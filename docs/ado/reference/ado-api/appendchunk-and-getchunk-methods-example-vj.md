---
title: "AppendChunk and GetChunk Methods Example (VJ++) | Microsoft Docs"
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
  - "AppendChunk method [ADO], VJ++ example"
  - "GetChunk method [ADO], VJ++ example"
ms.assetid: c21d0e82-81b3-4b06-a91e-77efad17c093
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "jhubbard"
---
# AppendChunk and GetChunk Methods Example (VJ++)
This example uses the [AppendChunk](../../../ado/reference/ado-api/appendchunk-method-ado.md) and [GetChunk](../../../ado/reference/ado-api/getchunk-method-ado.md) methods to fill an image field with data from another record.  
  
```  
// BeginAppendChunkJ  
import com.ms.wfc.data.*;  
import java.io.*;  
import com.ms.com.*;  
import java.util.*;  
  
public class AppendChunkX  
{  
  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      AppendChunkX();  
      System.exit(0);  
   }  
  
   // AppendChunkX function  
  
   static void AppendChunkX()  
   {  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Recordset rstPubInfo = null;  
  
      //Declarations.  
      String strCnn;  
      String strPubID;  
      String strPRInfo;  
      String strMessage = "";  
      long lngOffset = 0;  
      long lngLogoSize;  
      final  int conChunkSize = 100;  
      byte[] varChunk = new byte[conChunkSize];  
      int intCommand = 0 ;  
      int intMulChunkSize,intLastChunkSize;  
      Vector varLogo = new Vector();  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
      String line = null;  
      int noOfRecords;  
      int noOfRecordesDisplayed = 5;  
      int recordCount = 0;  
      String info;  
      int indexOfComma ;  
  
      try  
      {  
         // Open a connection.  
         strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
            + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn,"","",AdoEnums.CommandType.UNSPECIFIED);  
  
         // Open the pub_info Table.  
         rstPubInfo = new Recordset();  
         rstPubInfo.setCursorType(AdoEnums.CursorType.KEYSET );  
         rstPubInfo.setLockType(AdoEnums.LockType.OPTIMISTIC );  
  
         rstPubInfo.open("pub_info", cnConn1,AdoEnums.CursorType.KEYSET ,   
            AdoEnums.LockType.OPTIMISTIC, AdoEnums.CommandType.TABLE );  
         System.out.println ("Available logos are : \n");  
  
         noOfRecords = rstPubInfo.getRecordCount();  
  
         // Prompt for the Logo to copy.  
         for ( int i = 0; i < noOfRecords; i++)  
         {  
            recordCount++;  
            strMessage = strMessage +   
               rstPubInfo.getField("pub_id").getString() + "\n";  
            indexOfComma =   
               rstPubInfo.getField("pr_info").getString().indexOf(",");  
            info =   
               rstPubInfo.getField("pr_info").getString().substring(0,   
               indexOfComma );  
            strMessage = strMessage + info + "\n\n";  
            // Display five records at a time.  
            if ( recordCount == noOfRecordesDisplayed)  
            {  
               System.out.println(strMessage);  
               System.out.println("\n\nPress <Enter> to continue..");  
               line = in.readLine();  
               strMessage = "";  
               recordCount = 0;  
            }  
            rstPubInfo.moveNext();  
            // Display last records and exit if last record.  
            if(rstPubInfo.getEOF())  
            {  
               System.out.println(strMessage);  
               break;  
            }  
         }  
         System.out.println ("\nEnter the ID of a logo to copy :\n"  );  
         strPubID = in.readLine();  
  
         // Copy the logo to a variable in chunks.  
         rstPubInfo.setFilter("pub_id = '" + strPubID + "'");  
         lngLogoSize = rstPubInfo.getField("logo").getActualSize();  
         while (lngOffset < lngLogoSize)  
         {  
            varChunk = rstPubInfo.getField("logo")  
               .getByteChunk(conChunkSize);  
            int i = 0;  
            while (i < conChunkSize && varLogo.size() < (int)lngLogoSize)  
            {  
               varLogo.addElement(new Byte(varChunk[i]));  
               i++;  
            }  
            lngOffset =  lngOffset + conChunkSize ;  
         }  
            //Get the data for New ID from the user.  
            System.out.println   
               ("\nEnter a new pub ID [must be > 9899 & < 9999]:");  
            strPubID =  in.readLine().trim();  
  
            System.out.println ("\nEnter descriptive text :");  
            strPRInfo =  in.readLine().trim();  
  
            //Temporarily add new publisher to publishers table to   
            //avoid getting error foreign key constraint.  
            cnConn1.execute("INSERT publishers(pub_id) VALUES('" +   
               strPubID + "')");  
  
            //Add a new record.  
            rstPubInfo.addNew();  
            rstPubInfo.getField("pub_id").setString(strPubID);  
            rstPubInfo.getField("pr_info").setString(strPRInfo);  
  
            //Copy the selected logo to the new logo in chunks.  
            lngOffset = 0;  
  
            //Divide logosize in multiples of constant chunk size.  
            intMulChunkSize =   
               (varLogo.size()/conChunkSize) * conChunkSize;  
            intLastChunkSize = varLogo.size()- intMulChunkSize ;  
            byte[] arrChunk  = new byte[conChunkSize];  
            byte[] lastChunk = new byte[intLastChunkSize];  
  
            while ( lngOffset < varLogo.size () )  
            {  
               int ii = 0 ;  
               // Copy the logo in constant chunk size.  
               if ( (int)lngOffset < intMulChunkSize)  
               {  
                  while (ii < conChunkSize &&   
                     (int)lngOffset < varLogo.size () )  
                  {  
                     arrChunk[ii] =   
                        ((Byte)varLogo.elementAt  
                        ((int)lngOffset)).byteValue();  
                     ii++;  
                     lngOffset++;  
                  }  
                  rstPubInfo.getField("logo").appendChunk(arrChunk);  
               }  
               // Copy the last remaining chunk.  
               else  
               {  
                  while (ii < intLastChunkSize &&   
                     (int)lngOffset < varLogo.size () )  
                  {  
                     lastChunk[ii] =   
                        ((Byte)varLogo.elementAt  
                        ((int)lngOffset)).byteValue();  
                     ii++;  
                     lngOffset++;  
  
                  }  
                  rstPubInfo.getField("logo").appendChunk(lastChunk);  
               }  
  
            }  
  
            // Update the new recordset with new logo.  
  
            rstPubInfo.update();  
  
            //Show the newly added data.  
            System.out.println ("\nNew Record : " +   
               rstPubInfo.getField("pub_id").getString() + "\n");  
            System.out.println ("Description : " +   
               rstPubInfo.getField("pr_info").getString() + "\n");  
            System.out.println ("Logo Size : " +   
               rstPubInfo.getField("logo").getActualSize() );  
  
            System.out.println ("\n\nPress <Enter> key to continue.");  
            in.readLine();  
  
            //Delete new records because this is a demonstration.  
            rstPubInfo.requery();  
            cnConn1.execute("DELETE FROM pub_info WHERE  pub_id = '" +   
               strPubID + "'");  
            cnConn1.execute("DELETE FROM publishers WHERE  pub_id = '" +   
               strPubID + "'");  
      }  
      catch( AdoException ae )  
         {  
            // Notify user of any errors that result from ADO.  
  
            // Check for null pointer for connection object.  
            if(rstPubInfo.getActiveConnection()==null)  
                  System.out.println("Exception: " + ae.getMessage());  
            // As passing a Recordset, check for null pointer first.  
            if (rstPubInfo != null)  
            {  
               PrintProviderError(rstPubInfo.getActiveConnection());  
               PrintADOError(ae);  
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
         if (rstPubInfo != null)  
            if (rstPubInfo.getState() == 1)  
               rstPubInfo.close();     
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
  
   // PrintADOError Function  
  
   static void PrintADOError(AdoException ae)  
   {  
      System.out.println("\t Error Source = " + ae.getSource() + "\n");  
      System.out.println("\t Description = " + ae.getMessage() + "\n");  
   }  
}  
// EndAppendChunkJ  
```  
  
## See Also  
 [AppendChunk Method (ADO)](../../../ado/reference/ado-api/appendchunk-method-ado.md)   
 [Field Object](../../../ado/reference/ado-api/field-object.md)   
 [GetChunk Method (ADO)](../../../ado/reference/ado-api/getchunk-method-ado.md)