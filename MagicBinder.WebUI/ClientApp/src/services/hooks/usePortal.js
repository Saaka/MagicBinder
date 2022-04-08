import {useRef, useEffect} from "react";

export function usePortal(id) {
    const portalElementRef = useRef(null);

    useEffect(() => {
        if(!!portalElementRef.current) {
            const root = document.getElementById(id);
            root.appendChild(portalElementRef.current);
        }
        
        return function removePortalElement() {
            if (!!portalElementRef.current)
                portalElementRef.current.remove();
        };
    }, []);

    function getRootElem() {
        if (!portalElementRef.current) {
            portalElementRef.current = document.createElement("div");
        }
        return portalElementRef.current;
    }

    return getRootElem();
}