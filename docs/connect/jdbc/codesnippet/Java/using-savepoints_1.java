public static void executeTransaction(Connection con) {
   try {
      con.setAutoCommit(false);
      Statement stmt = con.createStatement();
      stmt.executeUpdate("INSERT INTO Production.ScrapReason(Name) VALUES('Correct width')");
      Savepoint save = con.setSavepoint();
      stmt.executeUpdate("INSERT INTO Production.ScrapReason(Name) VALUES('Wrong width')");
      con.rollback(save);
      con.commit();
      stmt.close();
      System.out.println("Transaction succeeded.");
   }
   catch (SQLException ex) {
      ex.printStackTrace();
      try {
         System.out.println("Transaction failed.");
         con.rollback();
      }
      catch (SQLException se) {
         se.printStackTrace();
      }
   }
}