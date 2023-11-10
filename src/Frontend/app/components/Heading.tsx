import PropTypes from 'prop-types';

function Heading(props : {children: React.ReactNode, className?: string}) {
  return <h1 className={'text-5xl font-bold' + ' ' + props.className}>{props.children}</h1>;
}

Heading.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Heading;
