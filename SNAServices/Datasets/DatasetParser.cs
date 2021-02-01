using SNAEntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SNAServices.Datasets
{
    enum TokenType { 
        Number,
        EOF,
        UNKNOWN
    }

    public class DatasetParserException : Exception {
        public DatasetParserException(string message) : base(message) { 
        }
    }

    public class DatasetParser
    {
        string input;
        int inputLength = -1;

        int position;
        int tokenValue;

        public DatasetParser(string input) {
            this.input = input;
            this.inputLength = input.Length;
            this.position = 0;
            this.tokenValue = -1;
            
        }

        private TokenType GetNextToken() {
            while (true)
            {
                if (position >= inputLength)
                    return TokenType.EOF;

                if ("1234567890".Contains(input[position]))
                    return GetNextNumToken();

                if (" \t\n\r".Contains(input[position]))
                {
                    SkipSpaces();
                    continue;
                }

                return TokenType.UNKNOWN;
            }
        }

        private TokenType GetNextNumToken()
        {
            StringBuilder token = new StringBuilder();

            while ("1234567890".Contains(input[position])) {
                token.Append(input[position++]);
                if(position >= inputLength)
                    break;
            }

            tokenValue = int.Parse(token.ToString());
            return TokenType.Number;
        }

        private void SkipSpaces()
        {
            while (" \t\n\r".Contains(input[position])) {
                if (++position >= inputLength)
                    break;
            }
        }

        public List<Link> Parse() {

            TokenType token;
            List<Link> result = new List<Link>();

            while (true)
            {
                token = GetNextToken();

                if (token == TokenType.UNKNOWN)
                    throw new DatasetParserException("Unknown token occured in dataset");
                if (token == TokenType.EOF)
                    break;

                int id1 = tokenValue;

                token = GetNextToken();

                if (token == TokenType.UNKNOWN)
                    throw new DatasetParserException("Unknown token occured in dataset");
                if (token == TokenType.EOF)
                    throw new DatasetParserException("Unexpected end of input");

                int id2 = tokenValue;

                result.Add(new Link()
                {
                    User1Id = id1,
                    User2Id = id2
                });
            }

            if(result.Count < 1)
                throw new DatasetParserException("Dataset must contain at least one records");
            return result;
        }
    }
}
