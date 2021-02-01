import React, { Component } from 'react';
import { NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <div className="container text-center">
            <h1>Welcome to the Social Network Analyser</h1>

            <img src={require('./images/logo.png')} alt="Logo" />
            <div className="container text-right">
                <NavLink tag={Link} to="/sna-datasets">Continue...</NavLink>
            </div>
            <div className="container fixed-bottom">ASP.NET Core 5.0, ReactJS, React Bootstrap, React Force Graph</div>
       </div>
        
    );
  }

   
}
