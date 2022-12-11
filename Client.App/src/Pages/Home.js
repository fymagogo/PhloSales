import React, { Component } from 'react';

export default class Dashboard extends Component {
    static displayName = Dashboard.name;

    constructor(props) {
        super(props);
        this.state = {
            products: [],
            customer: "",
            price: 0,
            selectedProductId: "",
            loading: true
        };
    }

    async getProductsData() {
        const response = await fetch('https://localhost:44379/api/Products');
        const data = await response.json();
        this.setState({ products: data, loading: false });
    }

    async componentDidMount() {
       await this.getProductsData();
    }

    handleSubmit = async (event) => {
        event.preventDefault();
        this.setState({ loading: true });
        const customerName = this.state.customer;
        const productId = this.state.selectedProductId;
        const price = Number(this.state.price);
        if (price === 0) {
           return alert("Enter a price");
        }
        await fetch('https://localhost:44379/api/Orders', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                customerName: customerName,
                price: price,
                productId: productId
            }),
        }).then((response) => response.json)
            .then((data) => {
                this.setState({
                    selectedProductId: "",
                    price: 0,
                    customer: "",
                    loading: false
                });
                alert("Successfully placed order");
            }).catch((data) => {
                console.log("post error:", data);
            });
    };

    render() {

        const selectOptions = this.state.products.map(product => {
            return (<option key={product.id} value={product.id}>{product.name}</option>)
        })

        return (
            <form onSubmit={this.handleSubmit}>
                <label>
                    Customer Name:
                    <input type="text" value={this.state.customer} onChange={event => this.setState({ customer: event.target.value })} required />
                </label>
                <select value={this.state.selectedProductId} onChange={event => this.setState({ selectedProductId: event.target.value })} required>
                    <option value="">Select a product</option>
                    {selectOptions}
                </select>
                <label>
                    Price:
                    <input type="number" value={this.state.price} onChange={event => this.setState({ price: event.target.value })} required/>
                </label>
                <input type="submit" value="Submit" />
            </form>
        );
    }

}
