import React, { Component } from 'react';

export default class Dashboard extends Component {
    static displayName = Dashboard.name;

    constructor(props) {
        super(props);
        this.state = { products: [], loading: true };
    }

    async componentDidMount() {
       await this.populateOrdersData();
    }

    static renderForecastsTable(products) {
        const currency = Intl.NumberFormat("en-US", { style: "currency", currency: "GHS" });
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
                        <tr key={product.id}>
                            <td>{product.name}</td>
                            <td>{product.numberOfOrders}</td>
                            <td>{currency.format(product.maxPrice)}</td>
                            <td>{currency.format(product.minPrice)}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        //this.populateOrdersData();
        let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. </em></p>
            : Dashboard.renderForecastsTable(this.state.products);

        return (
            <div>
                <h1 id="tabelLabel" >Product Orders</h1>
                <p>Find below the various products sold.</p>
                {contents}
            </div>
        );
    }

    async populateOrdersData() {
        const response = await fetch('https://localhost:44379/api/Products/sold');
        const data = await response.json();
        this.setState({ products: data, loading: false });
    }
}
