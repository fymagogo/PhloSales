import React, { Component } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Home from './Pages/Home';
import Dashboard from './Pages/Dashboard';
import './App.css';

class App extends Component {
	render() {
		return (
			<Router>
				<div className="App">
					<ul className="App-header">
						<li>
							<Link to="/">Home</Link>
						</li>
						<li>
							<Link to="/dashboard">Dashboard</Link>
						</li>
					</ul>
					<Routes>
						<Route exact path='/' element={< Home />}></Route>
						<Route exact path='/dashboard' element={< Dashboard />}></Route>
					</Routes>
				</div>
			</Router>
		);
	}
}

export default App;
