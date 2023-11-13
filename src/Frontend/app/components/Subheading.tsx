import PropTypes from 'prop-types';

export interface SubheadingProps {
	children: React.ReactNode;
  	className?: string;
}

function Subheading(props : {children: React.ReactNode, className?: string}) {
  return <h2 className={'text-2xl font-normal text-[#909090]' + ' ' + props.className}>{props.children}</h2>;
}

Subheading.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Subheading;
