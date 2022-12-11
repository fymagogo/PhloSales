import React, { Component } from 'react';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { products: [], loading: true };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    static renderForecastsTable(products) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Product</th>
                        <th>Number Of Orders</th>
                        <th>Max. Price</th>
                        <th>Min. Price</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product =>
                        <tr key={product.Id}>
                            <td>{product.Name}</td>
                            <td>{product.temperatureC}</td>
                            <td>{product.temperatureF}</td>
                            <td>{product.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : App.renderForecastsTable(this.state.products);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('/api/Products/sold');
        const data = await response.json();
        this.setState({ products: data, loading: false });
    }
}
