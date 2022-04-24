import React from "react";

const ResponsiveContainer = (props) => {

    return (
        <section className="columns is-centered is-mobile">
            <div className="column is-responsive">
                {props.children}
            </div>
        </section>
    )
};

export {ResponsiveContainer};