using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Pawn
    {
        private bool firstPlayer;
        private bool isKing;
	
        //constr, deep copy
	    public Pawn (bool firstPlayer) {
	    	this.firstPlayer = firstPlayer;
		    isKing = false;
	    }
	    public Pawn (Pawn pawn) {
	    	firstPlayer = pawn.isFirstPlayer();
	    	isKing = pawn.checkIfKing();
	    }	

        // get, set
	    public bool isFirstPlayer() {
	    	return firstPlayer;
	    }
	    public bool checkIfKing() {
	    	return isKing;
	    }
	    public void makeKing() {
	    	isKing = true;
	    }
    }
}
