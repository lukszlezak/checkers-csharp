using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Move
    {
        //pawn positions
        private int prevX;
        private int prevY;  
        private int newX;
        private int newY;

        //beaten pawn position
        private int beatenX;
        private int beatenY;
	    private bool isBeating;

        //multiple beating
	    private Move nextBeating;
        private Move prevBeating;
	
        //movement
	    public Move (int prevX, int prevY, int newX, int newY) {
		    this.prevX = prevX;
		    this.prevY = prevY;
		    this.newX = newX;
		    this.newY = newY;
		    isBeating = false;
	    } 
	
        //beating movement
	    public Move (int prevX, int prevY, int newX, int newY, int beatenX, int beatenY) {
	    	this.prevX = prevX;
	    	this.prevY = prevY;
	    	this.newX = newX;
	    	this.newY = newY;
		    this.beatenX = beatenX;
		    this.beatenY = beatenY;
		    isBeating = true;
	    }

        //multiple beating movement
        public Move (int prevX, int prevY, int newX, int newY, int beatenX, int beatenY, Move move) {
	    	this.prevX = prevX;
		    this.prevY = prevY;
		    this.newX = newX;
		    this.newY = newY;
		    this.beatenX = beatenX;
		    this.beatenY = beatenY;
		    isBeating = true;
		    prevBeating = move;
	    }
	
        //deep copy
	    public Move (Move move) {
	    	this.prevX = move.getPrevX();
		    this.prevY = move.getPrevY();
		    this.newX = move.getNewX();
		    this.newY = move.getNewY();
		    this.beatenX = move.getBeatingX();
		    this.beatenY = move.getBeatingY();
		    this.isBeating = move.getIsBeating();
		    this.prevBeating = move.getPrevBeating();
		    this.nextBeating = move.getNextBeating();
	    }
	
        //getters, setters
	    public int getPrevX() {
	    	return prevX;
	    }	
	    public int getPrevY() {
	    	return prevY;
	    }	
	    public int getNewX() {
	    	return newX;
	    }	
	    public int getNewY() {
	    	return newY;
	    }	
	    public int getBeatingX() {
	    	return beatenX;
	    }	
	    public int getBeatingY() {
	    	return beatenY;
	    }	
	    public bool getIsBeating() {
	    	return isBeating;
	    }	
	    public Move getNextBeating() {
	    	return nextBeating;
	    }	
	    public void setNextBeating (Move move) {
	    	nextBeating = move;
	    }
        public Move getPrevBeating()
        {
            return prevBeating;
        }
    }
}
