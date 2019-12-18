using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class AI
    {
        private int maxDepth;
        public int numberOfNodes;
        private bool alphaBeta;

        // 1-evaluate(), 2-evalKings, 3-evalEdges, 4-evalLevels
        private int evaluation;

        private Move bestMove;

        public AI(int maxDepth, bool alphaBeta, int evaluation)
        {
            this.maxDepth = maxDepth;
            this.alphaBeta = alphaBeta;
            this.evaluation = evaluation;
        }

        // returns best move after minmax search
        public Move choseMove(Board board, bool ifMaxMin)
        {
            //DateTime start = DateTime.Now;
            if(alphaBeta)
                alphaBetaSearch(board, ifMaxMin, 0, Int32.MinValue, Int32.MaxValue);
            else
                minMax(board, ifMaxMin, 0);
            //TimeSpan elapsedTime = DateTime.Now - start;
           // if(maxDepth>1)
           // Console.WriteLine(elapsedTime.TotalMilliseconds);
            return bestMove;
        }

        // pre-order tree traversal
        private int minMax(Board board, bool ifMaxMin, int depth)
        {
            ++numberOfNodes;
            if (depth == maxDepth && evaluation == 1)    
                return board.evaluate();
            if (depth == maxDepth && evaluation == 2)
                return board.evalKings();
            if (depth == maxDepth && evaluation == 3)
                return board.evalEdges();
            if (depth == maxDepth && evaluation == 4)
                return board.evalLevels();

            if (board.endOfGame() == 2)
                return -10000;
            if (board.endOfGame() == 1)
                return 10000;           
            List<Move> possibleMovements = new List<Move>();
            board.checkBeatings(ifMaxMin, possibleMovements);
            if (!possibleMovements.Any()) 
                board.getMovements(ifMaxMin, possibleMovements);

            if (ifMaxMin)
            {
                int eval = Int32.MinValue;
                foreach (Move move in possibleMovements)
                {
                    int temporaryEval = minMax(new Board(board, move), !ifMaxMin, depth + 1);
                    if (temporaryEval > eval)
                    {
                        eval = temporaryEval;
                        if (depth == 0)
                        bestMove = move;
                    }
                }
                return eval;
            }
            else
            {
                int eval = Int32.MaxValue;
                foreach (Move move in possibleMovements)
                {
                    int temporaryEval = minMax(new Board(board, move), !ifMaxMin, depth + 1);
                    if (temporaryEval < eval)
                    {
                        eval = temporaryEval;
                        if (depth == 0)
                        bestMove = move;
                    }
                }
                return eval;
            }
        }

        private int alphaBetaSearch (Board board, bool isMaxMin, int depth, int alpha, int beta) {
            ++numberOfNodes;
            if (board.endOfGame() == 2)
                return -10000;
		    if (board.endOfGame() == 1)
                return 10000;
		    if (depth == maxDepth && evaluation == 1)    
                return board.evaluate();
            if (depth == maxDepth && evaluation == 2)
                return board.evalKings();
            if (depth == maxDepth && evaluation == 3)
                return board.evalEdges();
            if (depth == maxDepth && evaluation == 4)
                return board.evalLevels();

		    List <Move> possibleMovements = new List <Move> ();
		    board.checkBeatings (isMaxMin, possibleMovements);
		    if (!possibleMovements.Any()) 
                board.getMovements (isMaxMin, possibleMovements);
            
            possibleMovements = sortMovements(possibleMovements);  

		    if (isMaxMin) { 
				foreach (Move move in possibleMovements) {
					int temporaryEval = alphaBetaSearch (new Board (board, move), !isMaxMin, depth+1, alpha, beta);
					if (temporaryEval > alpha) {
						alpha = temporaryEval;
                        if (depth == 0)
						    bestMove = move;
				    }
                    if (alpha >= beta) return beta;
			    }
				return alpha;
		    }
		    else {
				foreach (Move move in possibleMovements) {
					int temporaryEval = alphaBetaSearch (new Board (board, move), !isMaxMin, depth+1, alpha, beta);
					if (temporaryEval < beta) {
						beta = temporaryEval;
                        if (depth == 0)
						     bestMove = move;
						}
                    if (alpha >= beta) return alpha;
				}
				return beta;
			}    
	    }

        List<Move> sortMovements(List<Move> movements)
        {
            List<Move> movementsSorted = new List<Move>();
            foreach (Move move in movements)
            {
                if (move.getNextBeating() != null && move.getNextBeating().getNextBeating() != null && move.getNextBeating().getNextBeating().getNextBeating() != null)
                {
                    movementsSorted.Add(move);
                }
            }
             foreach (Move move in movements)
            {
                if (move.getNextBeating() != null && move.getNextBeating().getNextBeating() != null)
                {
                    movementsSorted.Add(move);
                }
            }
             foreach (Move move in movements)
             {
                 if (move.getNextBeating() != null)
                 {
                     movementsSorted.Add(move);
                 }
             }
            foreach (Move move in movements)
            {
                if (move.getNextBeating() == null)
                {
                    movementsSorted.Add(move);
                }
            }
            return movementsSorted;
        }
    }
}