public static void executeTransaction(Connection con) {
    try (Statement stmt = con.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);) {
        con.setAutoCommit(false);
        con.setHoldability(ResultSet.HOLD_CURSORS_OVER_COMMIT);

        stmt.executeUpdate("INSERT INTO Production.ScrapReason(Name) VALUES('Bad part')");
        ResultSet rs = stmt.executeQuery("SELECT * FROM Production.ScrapReason");
        con.commit();
        System.out.println("Transaction succeeded.");

        // Display results.
        while (rs.next()) {
            System.out.println(rs.getString(2));
        }
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
